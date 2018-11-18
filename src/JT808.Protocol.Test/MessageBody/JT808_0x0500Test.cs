﻿using JT808.Protocol.MessageBody;
using JT808.Protocol.MessageBody.JT808LocationAttach;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;
using System.Diagnostics;
using Xunit.Extensions;

namespace JT808.Protocol.Test.MessageBodyReply
{
    public class JT808_0x0500Test
    {
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId.车辆控制应答.ToUInt16Value(),
                MsgNum = 8888,
                TerminalPhoneNo = "112233445566",
            };
            JT808_0x0500 jT808_0X0500 = new JT808_0x0500();
            JT808_0x0200 JT808_0x0200_1 = new JT808_0x0200();
            JT808_0x0200_1.AlarmFlag = 1;
            JT808_0x0200_1.Altitude = 40;
            JT808_0x0200_1.GPSTime = DateTime.Parse("2018-07-15 10:10:10");
            JT808_0x0200_1.Lat = 12222222;
            JT808_0x0200_1.Lng = 132444444;
            JT808_0x0200_1.Speed = 60;
            JT808_0x0200_1.Direction = 0;
            JT808_0x0200_1.StatusFlag = 2;
            JT808_0x0200_1.JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>();
            JT808_0x0200_1.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x01, new JT808LocationAttachImpl0x01
            {
                Mileage = 100
            });
            JT808_0x0200_1.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x02, new JT808LocationAttachImpl0x02
            {
                Oil = 55
            });
            jT808_0X0500.JT808_0x0200 = JT808_0x0200_1;
            jT808_0X0500.MsgNum = 1000;
            jT808Package.Bodies = jT808_0X0500;
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0500002811223344556622B803E8000000010000000200BA7F0E07E4F11C0028003C000018071510101001040000006402020037B57E".Length, hex.Length);
            Assert.Equal("7E0500002811223344556622B803E8000000010000000200BA7F0E07E4F11C0028003C000018071510101001040000006402020037B57E", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "7E0500002811223344556622B803E8000000010000000200BA7F0E07E4F11C0028003C000018071510101001040000006402020037B57E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.车辆控制应答.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(8888, jT808Package.Header.MsgNum);
            Assert.Equal("112233445566", jT808Package.Header.TerminalPhoneNo);
            JT808_0x0500 JT808Bodies = (JT808_0x0500)jT808Package.Bodies;
            Assert.Equal(1000, JT808Bodies.MsgNum);
            Assert.Equal((uint)1, JT808Bodies.JT808_0x0200.AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), JT808Bodies.JT808_0x0200.GPSTime);
            Assert.Equal(12222222, JT808Bodies.JT808_0x0200.Lat);
            Assert.Equal(132444444, JT808Bodies.JT808_0x0200.Lng);
            Assert.Equal(0, JT808Bodies.JT808_0x0200.Direction);
            Assert.Equal(60, JT808Bodies.JT808_0x0200.Speed);
            Assert.Equal((uint)2, JT808Bodies.JT808_0x0200.StatusFlag);
            Assert.Equal(100, ((JT808LocationAttachImpl0x01)JT808Bodies.JT808_0x0200.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x01]).Mileage);
            Assert.Equal(55, ((JT808LocationAttachImpl0x02)JT808Bodies.JT808_0x0200.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x02]).Oil);
        }
    }
}
