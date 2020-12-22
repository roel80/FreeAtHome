﻿/*
 *  FreeAtHome KNX VirtualSwitch and Communication module. This software
    provides interaction over KNX to Free@Home bus devices.

    This software is not created, maintained or has any assosiation
    with ABB \ Busch-Jeager.

    Copyright (C) 2020 Roeland Kluit

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    The Software is provided to you by the Licensor under the License,
    as defined, subject to the following condition.

    Without limiting other conditions in the License, the grant of rights
    under the License will not include, and the License does not grant to
    you, the right to Sell the Software.

    For purposes of the foregoing, "Sell" means practicing any or all of
    the rights granted to you under the License to provide to third
    parties, for a fee or other consideration (including without
    limitation fees for hosting or consulting/ support services related
    to the Software), a product or service whose value derives, entirely
    or substantially, from the functionality of the Software.
    Any license notice or attribution required by the License must also
    include this Commons Clause License Condition notice.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.

    This modules is a custom representation of the data in a Free@home device, can be serialized as Json.
    Please note not all fields are reverse engineerd.
    
*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeAtHomeDevices
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
    public class FahSystemID
    {
        [System.Runtime.InteropServices.FieldOffset(0)]
        public UInt16 SystemID;

        [JsonIgnore]
        [System.Runtime.InteropServices.FieldOffset(0)]
        public byte SystemIDHigh;

        [JsonIgnore]
        [System.Runtime.InteropServices.FieldOffset(1)]
        public byte SystemIDLow;

        public FahSystemID()
        {
            SystemID = 0;
        }

        public FahSystemID(byte lowvalue, byte highvalue)
        {
            SystemIDHigh = highvalue;
            SystemIDLow = lowvalue;
        }

        [JsonIgnore]
        public byte[] ByteArray
        {
            get
            {
                return new byte[] { SystemIDLow, SystemIDHigh };
            }
        }

        public FahSystemID(UInt16 value)
        {
            SystemIDHigh = 0;
            SystemIDLow = 0;
            SystemID = value;
        }

        public static FahSystemID FromByteArray(byte[] input, UInt16 begin)
        {
            if (input.Length - begin < 2)
                throw new InternalBufferOverflowException();

            byte[] target = new byte[2];


            FahSystemID n = new FahSystemID
            {
                SystemID = BitConverter.ToUInt16(input, begin)
            };
            return n;
        }

        public override string ToString()
        {
            return SystemID.ToString(); // string.Format("[0x{0:X2}-0x{1:X2}]", SystemIDLow, SystemIDHigh);
        }
    }
}
