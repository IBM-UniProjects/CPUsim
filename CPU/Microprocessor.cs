using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CPU
{
    public class Microprocessor
    {
        private SortedDictionary<string, SortedDictionary<string, bool[]>> registers;

        public Microprocessor()
        {
            registers = InitRegisters();
        }

        private SortedDictionary<string, SortedDictionary<string, bool[]>> InitRegisters()
        {
            return new SortedDictionary<string, SortedDictionary<string, bool[]>>
            {
                ["AX"] = new SortedDictionary<string, bool[]>
                {
                    ["AH"] = Enumerable.Repeat(false, 8).ToArray(),
                    ["AL"] = Enumerable.Repeat(false, 8).ToArray()
                },
                ["BX"] = new SortedDictionary<string, bool[]>
                {
                    ["BH"] = Enumerable.Repeat(false, 8).ToArray(),
                    ["BL"] = Enumerable.Repeat(false, 8).ToArray()
                },
                ["CX"] = new SortedDictionary<string, bool[]>
                {
                    ["CH"] = Enumerable.Repeat(false, 8).ToArray(),
                    ["CL"] = Enumerable.Repeat(false, 8).ToArray()
                },
                ["DX"] = new SortedDictionary<string, bool[]>
                {
                    ["DH"] = Enumerable.Repeat(false, 8).ToArray(),
                    ["DL"] = Enumerable.Repeat(false, 8).ToArray()
                }
            };
        }

        public void Command(string cmd)
        {
            string[] cmdWords = cmd.Split();
            cmd = cmdWords[0].Substring(0, 3);

            if (cmdWords.Length != 3 && cmd != "INT")
                throw new FormatException("Command must have exactly 2 arguments.");
            
            switch (cmd)
            {
                case "MOV":
                    MOV(cmdWords.Skip(1).ToArray());
                    break;
                case "ADD":
                    ADD(cmdWords.Skip(1).ToArray());
                    break;
                case "SUB":
                    SUB(cmdWords.Skip(1).ToArray());
                    break;
                case "INT":
                    INT(cmdWords[0]);
                    break;
                default:
                    throw new NotSupportedException("Not supported command.");
            }
        }

        private void INT(string interrupt) {
            switch (interrupt)
            {
                case "INT1":
                    int n = RegToInt("AH");
                    Console.WriteLine(n);
                    bool[] reg = IntToReg(n);
                    for (int i = 0; i < reg.Length; i++)
                    {
                        Console.Write(reg[i] ? 1 : 0);
                    }
                    Console.WriteLine();
                    break;
                case "INT33":
                    INT33();
                    break;
                default:
                    throw new NotSupportedException(String.Format("Not supported interrupt {0}.", interrupt));
            }
        }

        private void INT33()
        {
            int AH = RegToInt("AH");
            int AL;
            int CH;
            int CL;
            int DH;
            int DL;
            switch (AH)
            {
                case 1:
                    /* AH = 01h
                     * Read character from stdin
                     * Return: AL = character read
                     */
                    FlushStdin();
                    AL = Console.ReadKey().KeyChar;
                    Console.WriteLine("\n" + AL);
                    FlushStdin();
                    if (AL > 255)
                        AL = 0;
                    registers["AX"]["AL"] = IntToReg(AL);
                    break;
                case 2:
                    /* AH = 02h
                     * Write character to stdout
                     * Entry: DL = character to write
                     * Return: AL = last character output
                     */
                    DL = RegToInt("DL");
                    Console.WriteLine(Convert.ToChar(DL));
                    if (DL == 9)
                        AL = 32;
                    else
                        AL = DL;
                    registers["AX"]["AL"] = IntToReg(AL);
                    break;
                case 44:
                    /* AH = 2Ch
                     * Get system time
                     * Return: CH: hour, CL: minute, DH: second, DL: 1/100 second
                     */
                    DateTime time = DateTime.Now;
                    CH = time.Hour;
                    CL = time.Minute;
                    DH = time.Second;
                    DL = time.Millisecond / 10;
                    Console.WriteLine(time.ToString("HH:mm:ss:ff"));
                    registers["CX"]["CH"] = IntToReg(CH);
                    registers["CX"]["CL"] = IntToReg(CL);
                    registers["DX"]["DH"] = IntToReg(DH);
                    registers["DX"]["DL"] = IntToReg(DL);
                    break;
                default:
                    throw new NotSupportedException(String.Format("Not supported interrupt INT33, AH = {0}.", AH));
            }
        }

        private void FlushStdin()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        private int RegToInt(string register)
        {
            bool[] reg;
            int n = 0;

            if (IsGPR(register))
                throw new FormatException("16-bit register not supported.");

            string gpr = register[0] + "X";
            reg = (bool[]) registers[gpr][register].Clone();

            for (int i = 0; i < reg.Length; i++)
            {
                n = (n << 1) + (reg[i] ? 1 : 0);
            }

            return n;
        }

        private bool[] IntToReg(int n)
        {
            if (n > 255)
                throw new FormatException("Max 8-bit number.");

            bool[] reg = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                reg[7 - i] = (1 << i & n) != 0;
            }
            return reg;
        }

        private void MOV(string[] arguments)
        {
            if (!IsRegister(arguments[1]))
                throw new FormatException("Register does not exist.");

            if (IsRegister(arguments[0]))
            {
                if (!IsSameType(arguments[0], arguments[1]))
                    throw new FormatException("Registers have different sizes.");
            }
            else if (!IsSameSize(arguments[0], arguments[1]))
                throw new FormatException("Value does not match register size.");

            string destRegister = arguments[1];
            bool[] sourceValue;

            if (IsRegister(arguments[0]))
                sourceValue = GetValueFromRegister(arguments[0]);
            else if (Regex.IsMatch(arguments[0], "^[01]+$"))
                sourceValue = arguments[0].Select(c => c == '1').ToArray();
            else
                throw new FormatException("First argument is not a register nor an 8/16 bits number.");

            WriteToRegister(sourceValue, destRegister);
        }

        private void ADD(string[] arguments)
        {
            if (!IsRegister(arguments[1]))
                throw new FormatException("Register does not exist.");

            if (IsRegister(arguments[0]))
            {
                if (!IsSameType(arguments[0], arguments[1]))
                    throw new FormatException("Registers have different sizes.");
            }
            else if (!IsSameSize(arguments[0], arguments[1]))
                throw new FormatException("Value does not match register size.");

            string destRegister = arguments[1];
            bool[] sourceValue;

            if (IsRegister(arguments[0]))
                sourceValue = GetValueFromRegister(arguments[0]);
            else if (Regex.IsMatch(arguments[0], "^[01]+$"))
                sourceValue = arguments[0].Select(c => c == '1').ToArray();
            else
                throw new FormatException("First argument is not a register nor an 8/16 bits number.");

            AddToRegister(sourceValue, destRegister);
        }

        private void SUB(string[] arguments)
        {
            if (!IsRegister(arguments[1]))
                throw new FormatException("Register does not exist.");

            if (IsRegister(arguments[0]))
            {
                if (!IsSameType(arguments[0], arguments[1]))
                    throw new FormatException("Registers have different sizes.");
            }
            else if (!IsSameSize(arguments[0], arguments[1]))
                throw new FormatException("Value does not match register size.");

            string destRegister = arguments[1];
            bool[] sourceValue;

            if (IsRegister(arguments[0]))
                sourceValue = GetValueFromRegister(arguments[0]);
            else if (Regex.IsMatch(arguments[0], "^[01]+$"))
                sourceValue = arguments[0].Select(c => c == '1').ToArray();
            else
                throw new FormatException("First argument is not a register nor an 8/16 bits number.");

            SubFromRegister(sourceValue, destRegister);
        }

        private bool IsSameSize(string value, string register)
        {
            if (IsGPR(register))
                return value.Length == 16;
            else
                return value.Length == 8;
        }

        private bool IsSameType(string register1, string register2)
        {
            if ((IsGPR(register1) && IsGPR(register2)) || (!IsGPR(register1) && !IsGPR(register2)))
                return true;
            else
                return false;
        }

        private void WriteToRegister(bool[] value, string register)
        {
            if (IsGPR(register))
            {
                string rH = register[0] + "H";
                string rL = register[0] + "L";
                registers[register][rH] = value.Take(8).Cast<bool>().ToArray();
                registers[register][rL] = value.Skip(8).Cast<bool>().ToArray();
            }
            else
            {
                string gpr = register[0] + "X";
                registers[gpr][register] = (bool[]) value.Clone();
            }
        }

        private void AddToRegister(bool[] value, string register)
        {
            if (IsGPR(register))
            {
                string rH = register[0] + "H";
                string rL = register[0] + "L";
                bool[] registerValue = registers[register][rH].Concat(registers[register][rL]).ToArray();
                registerValue = Adder(value, registerValue);
                registers[register][rH] = registerValue.Take(8).Cast<bool>().ToArray();
                registers[register][rL] = registerValue.Skip(8).Cast<bool>().ToArray();
            }
            else
            {
                string gpr = register[0] + "X";
                bool[] registerValue = registers[gpr][register];
                registerValue = Adder(value, registerValue);
                registers[gpr][register] = (bool[]) registerValue.Clone();
            }
        }

        private bool[] Adder(bool[] a, bool[] b)
        {
            var sum = new List<bool>(Enumerable.Repeat(false, a.Length));
            bool carry = false;
            for (int i = a.Length - 1; i >= 0; i--)
            {
                sum[i] = (a[i] ^ b[i]) ^ carry;
                carry = (a[i] && b[i]) || (carry && (a[i] ^ b[i]));
            }

            return sum.ToArray();
        }

        private void SubFromRegister(bool[] value, string register)
        {
            if (IsGPR(register))
            {
                string rH = register[0] + "H";
                string rL = register[0] + "L";
                bool[] registerValue = registers[register][rH].Concat(registers[register][rL]).ToArray();
                registerValue = Subtractor(value, registerValue);
                registers[register][rH] = registerValue.Take(8).Cast<bool>().ToArray();
                registers[register][rL] = registerValue.Skip(8).Cast<bool>().ToArray();
            }
            else
            {
                string gpr = register[0] + "X";
                bool[] registerValue = registers[gpr][register];
                registerValue = Subtractor(value, registerValue);
                registers[gpr][register] = (bool[])registerValue.Clone();
            }
        }

        private bool[] Subtractor(bool[] a, bool[] b) // b - a
        {
            var diff = new List<bool>(Enumerable.Repeat(false, a.Length));
            bool borrow = false;
            for (int i = a.Length - 1; i >= 0; i--)
            {
                diff[i] = (a[i] ^ b[i]) ^ borrow;
                borrow = (borrow && !(a[i] ^ b[i])) || (a[i] && !b[i]);
            }

            return diff.ToArray();
        }

        private bool[] GetValueFromRegister(string register)
        {
            if (IsGPR(register))
            {
                return (bool[]) registers[register][register[0] + "H"].Concat(registers[register][register[0] + "L"]);
            }
            else
            {
                return registers[register[0] + "X"][register];
            }
        }

        private bool IsRegister(string address)
        {
            if (registers.ContainsKey(address)) return true;
            else if ( registers.ContainsKey(address[0] + "X") && registers[address[0] + "X"].ContainsKey(address)) return true;
            else return false;
        }

        private bool IsGPR(string register)
        {
            return register[1].Equals('X');
        }

        public void PrintRegistersToConsole()
        {
            foreach(var gpRegister in registers)
            {
                Console.WriteLine("{0}:", gpRegister.Key);
                foreach(var register in gpRegister.Value)
                {
                    Console.Write(" {0}:", register.Key);
                    foreach(bool value in register.Value)
                    {
                        Console.Write("{0}", value ? 1 : 0);
                    }
                }
                Console.WriteLine();
            }
        }

        public string PrintRegistersToString()
        {
            string pRegisters = "";
            foreach (var gpRegister in registers)
            {
                pRegisters += string.Format("{0}:\n", gpRegister.Key);
                foreach (var register in gpRegister.Value)
                {
                    pRegisters += string.Format("\t{0}:", register.Key);
                    foreach (bool value in register.Value)
                    {
                        pRegisters += string.Format("{0}", value ? 1 : 0);
                    }
                }
                pRegisters += "\n";
            }
            return pRegisters;
        }

        public void Clear()
        {
            registers = InitRegisters();
        }
    }
}
