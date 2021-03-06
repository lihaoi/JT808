﻿using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 休眠时汇报距离间隔，单位为米（m），>0
    /// </summary>
    public class JT808_0x8103_0x002E : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x002E>
    {
        public override uint ParamId { get; set; } = 0x002E;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 休眠时汇报距离间隔，单位为米（m），>0
        /// </summary>
        public uint ParamValue { get; set; }
        public JT808_0x8103_0x002E Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x002E jT808_0x8103_0x002E = new JT808_0x8103_0x002E();
            jT808_0x8103_0x002E.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x002E.ParamLength = reader.ReadByte();
            jT808_0x8103_0x002E.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x002E;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x002E value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
