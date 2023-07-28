using System;
using System.Collections.Generic;

namespace Emulator___July_2023
{
    class Program
    {
        static ushort[] RAM = new ushort[65536];
        static ushort[] Registers = new ushort[32];

        static Dictionary<byte, int> WhichRegister = new Dictionary<byte, int>()
        {
            [0x00] = 0,
            [0x01] = 1,
            [0x02] = 2,
            [0x03] = 3,
            [0x04] = 4,
            [0x05] = 5,
            [0x06] = 6,
            [0x07] = 7
        };

        static void ADD(byte[] instruction)
        {
            int destinationIndex = WhichRegister[instruction[1]];
            int src1Index = WhichRegister[instruction[2]];
            int src2Index = WhichRegister[instruction[3]];

            Registers[destinationIndex] = (ushort)(Registers[src1Index] + Registers[src2Index]);
        }
        static void SUB(byte[] instruction)
        {
            int destinationIndex = WhichRegister[instruction[1]];
            int src1Index = WhichRegister[instruction[2]];
            int src2Index = WhichRegister[instruction[3]];

            Registers[destinationIndex] = (ushort)(Registers[src1Index] + Registers[src2Index]);
        }
        static void MUL(byte[] instruction)
        {
            int destinationIndex = WhichRegister[instruction[1]];
            int src1Index = WhichRegister[instruction[2]];
            int src2Index = WhichRegister[instruction[3]];

            Registers[destinationIndex] = (ushort)(Registers[src1Index] * Registers[src2Index]);
        }
        static void DIV(byte[] instruction)
        {
            int destinationIndex = WhichRegister[instruction[1]];
            int src1Index = WhichRegister[instruction[2]];
            int src2Index = WhichRegister[instruction[3]];

            Registers[destinationIndex] = (ushort)(Registers[src1Index] / Registers[src2Index]);
        }
        static void MOD(byte[] instruction)
        {
            int destinationIndex = WhichRegister[instruction[1]];
            int src1Index = WhichRegister[instruction[2]];
            int src2Index = WhichRegister[instruction[3]];

            Registers[destinationIndex] = (ushort)(Registers[src1Index] % Registers[src2Index]);
        }
        static void NOT(byte[] instruction)
        {
            int destinationIndex = WhichRegister[instruction[1]];
            int src1Index = WhichRegister[instruction[2]];

            Registers[destinationIndex] = (ushort)(~Registers[src1Index]);
        }
        static void AND(byte[] instruction)
        {
            int destinationIndex = WhichRegister[instruction[1]];
            int src1Index = WhichRegister[instruction[2]];
            int src2Index = WhichRegister[instruction[3]];

            Registers[destinationIndex] = (ushort)(Registers[src1Index] & Registers[src2Index]);
        }
        static void OR(byte[] instruction)
        {
            int destinationIndex = WhichRegister[instruction[1]];
            int src1Index = WhichRegister[instruction[2]];
            int src2Index = WhichRegister[instruction[3]];

            Registers[destinationIndex] = (ushort)(Registers[src1Index] | Registers[src2Index]);
        }
        static void XOR(byte[] instruction)
        {
            int destinationIndex = WhichRegister[instruction[1]];
            int src1Index = WhichRegister[instruction[2]];
            int src2Index = WhichRegister[instruction[3]];

            Registers[destinationIndex] = (ushort)(Registers[src1Index] ^ Registers[src2Index]);
        }
        static void EQ(byte[] instruction)
        {
            int destinationIndex = WhichRegister[instruction[1]];
            int src1Index = WhichRegister[instruction[2]];
            int src2Index = WhichRegister[instruction[3]];

            if (Registers[src1Index] == Registers[src2Index])
            {
                Registers[destinationIndex] = 1;
            }
            else
            {
                Registers[destinationIndex] = 0;
            }
        }
        static void NEQ(byte[] instruction)
        {
            int destinationIndex = WhichRegister[instruction[1]];
            int src1Index = WhichRegister[instruction[2]];
            int src2Index = WhichRegister[instruction[3]];

            if(Registers[src1Index] != Registers[src2Index])
            {
                Registers[destinationIndex] = 1;
            }
            else
            {
                Registers[destinationIndex] = 0;
            }
        }
        static void GTE(byte[] instruction)
        {
            int destinationIndex = WhichRegister[instruction[1]];
            int src1Index = WhichRegister[instruction[2]];
            int src2Index = WhichRegister[instruction[3]];

            if(Registers[src1Index] >= Registers[src2Index])
            {
                Registers[destinationIndex] = 1;
            }
            else
            {
                Registers[destinationIndex] = 0;
            }
        }
        static void LTE(byte[] instruction)
        {
            int destinationIndex = WhichRegister[instruction[1]];
            int src1Index = WhichRegister[instruction[2]];
            int src2Index = WhichRegister[instruction[3]];

            if (Registers[src1Index] <= Registers[src2Index])
            {
                Registers[destinationIndex] = 1;
            }
            else
            {
                Registers[destinationIndex] = 0;
            }
        }
        static void GT(byte[] instruction)
        {
            int destinationIndex = WhichRegister[instruction[1]];
            int src1Index = WhichRegister[instruction[2]];
            int src2Index = WhichRegister[instruction[3]];

            if(Registers[src1Index] > Registers[src2Index])
            {
                Registers[destinationIndex] = 1;
            }
            else
            {
                Registers[destinationIndex] = 0;
            }
        }
        static void LT(byte[] instruction)
        {
            int destinationIndex = WhichRegister[instruction[1]];
            int src1Index = WhichRegister[instruction[2]];
            int src2Index = WhichRegister[instruction[3]];

            if (Registers[src1Index] < Registers[src2Index])
            {
                Registers[destinationIndex] = 1;
            }
            else
            {
                Registers[destinationIndex] = 0;
            }
        }
        static void SET(byte[] instruction)
        {
            int locationIndex = WhichRegister[instruction[1]];
            ushort valueHighByte = (ushort)(instruction[2] << 8);

            Registers[locationIndex] = (ushort)(valueHighByte | instruction[3]);
        }
        static void COPY(byte[] instruction)
        {
            int destinationIndex = WhichRegister[instruction[1]];
            int srcIndex = WhichRegister[instruction[2]];

            Registers[destinationIndex] = Registers[srcIndex]; 
        }
        static void Main(string[] args)
        {
            byte[] machineCode = System.IO.File.ReadAllBytes(args[0]);

            int instructionPointer = 0;
            List<byte> instruction = new List<byte>();

            for(int aByte = 0; aByte < machineCode.Length; aByte++)
            {
                instruction.Add(machineCode[aByte]);
                
                if((aByte + 1) % 4 == 0)
                {
                    switch (instruction[0])
                    {                 
                        case 0x10:
                            ADD(instruction.ToArray());
                            break;
                        case 0x11:
                            SUB(instruction.ToArray());
                            break;
                        case 0x12:
                            MUL(instruction.ToArray());
                            break;
                        case 0x13:
                            DIV(instruction.ToArray());
                            break;
                        case 0x14:
                            MOD(instruction.ToArray());
                            break;

                        case 0x20:
                            NOT(instruction.ToArray());
                            break;
                        case 0x21:
                            AND(instruction.ToArray());
                            break;
                        case 0x22:
                            OR(instruction.ToArray());
                            break;
                        case 0x23:
                            XOR(instruction.ToArray());
                            break;
                        case 0x24:
                            EQ(instruction.ToArray());
                            break;
                        case 0x25:
                            NEQ(instruction.ToArray());
                            break;
                        case 0x26:
                            GTE(instruction.ToArray());
                            break;
                        case 0x27:
                            LTE(instruction.ToArray());
                            break;
                        case 0x28:
                            GT(instruction.ToArray());
                            break;
                        case 0x29:
                            LT(instruction.ToArray());
                            break;

                        case 0x40:
                            SET(instruction.ToArray());
                            break;
                        case 0x41:
                            COPY(instruction.ToArray());
                            break;
                    }
                    instructionPointer++;
                    instruction.Clear();
                }
            }
        }
    }
}
