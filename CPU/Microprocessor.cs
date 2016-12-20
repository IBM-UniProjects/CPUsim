using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CPU
{
    class Microprocessor
    {
        public readonly SortedDictionary<string, SortedDictionary<string, bool[]>> registers;

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
            cmd = cmdWords[0];
            switch (cmd)
            {
                case "MOV":
                    MOV(cmdWords.Skip(1).ToArray());
                    break;
                case "ADD":
                    ADD(cmdWords.Skip(1).ToArray());
                    break;
                case "SUB":
                    //SUB(cmdWords.Skip(1).ToArray());
                    break;
                default:
                    throw new NotSupportedException("Not supported command.");
            }
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

        public void PrintRegisters()
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
    }
}
