﻿// See https://aka.ms/new-console-template for more information
using CRC_Packet;

byte[] rawData = new byte[]
      {
            0x40, 0x40,0x56, 0x00, 0x32, 0x47, 0x51, 0x2d, 0x31, 0x36, 0x30, 0x31, 0x30, 0x36, 0x33, 0x38, 0x01, 0x10, 0x06,
            0x0a, 0x13,0x0a, 0x2a, 0x08, 0x0f, 0x3e, 0x40, 0x44, 0x0a, 0x70, 0x89, 0xe6, 0x16, 0xcf, 0x00, 0x50, 0x04, 0xd0,
            0x31, 0x01,0x01, 0x05, 0x05, 0x05, 0x00, 0x06, 0x00, 0x01, 0x01, 0x00, 0x00, 0x18, 0xa5, 0xa6, 0xa7, 0xa8, 0xa9,
            0xaa, 0xf2,0xad, 0xae, 0xaf, 0xb0, 0xb1, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6a, 0x6b, 0x6c,
            0x06, 0x0a,0x13, 0x0a, 0x2a, 0x07, 0x53, 0xe8, 0x0d, 0x0a
      };

try
{
    Packet a = new Packet(rawData);

}

catch(Exception e)
{
    Console.WriteLine(e.ToString());
}





