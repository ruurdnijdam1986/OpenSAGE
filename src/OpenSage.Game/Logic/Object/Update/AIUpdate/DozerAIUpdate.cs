﻿using System.Collections.Generic;
using System.Numerics;
using OpenSage.Data.Ini;
using OpenSage.Mathematics;

namespace OpenSage.Logic.Object
{
    public sealed class DozerAIUpdate : AIUpdate
    {
        private readonly DozerSomething1[] _unknownList1 = new DozerSomething1[3];
        private readonly WorkerAIUpdateStateMachine1 _stateMachine = new();
        private int _unknown2;
        private readonly DozerSomething2[] _unknownList2 = new DozerSomething2[9];
        private int _unknown4;

        internal DozerAIUpdate(GameObject gameObject, DozerAIUpdateModuleData moduleData)
            : base(gameObject, moduleData)
        {
        }

        internal override void Load(SaveFileReader reader)
        {
            reader.ReadVersion(1);

            base.Load(reader);

            // Following is same as WorkerAIUpdate.Load

            var unknown1 = reader.ReadInt32();
            if (unknown1 != 3)
            {
                throw new InvalidStateException();
            }

            for (var i = 0; i < _unknownList1.Length; i++)
            {
                _unknownList1[i] = new DozerSomething1
                {
                    ObjectId = reader.ReadObjectID(),
                    Unknown = reader.ReadInt32()
                };
            }

            _stateMachine.Load(reader);

            _unknown2 = reader.ReadInt32();

            var unknown3 = reader.ReadInt32();
            if (unknown3 != 3)
            {
                throw new InvalidStateException();
            }

            for (var i = 0; i < _unknownList2.Length; i++)
            {
                _unknownList2[i] = new DozerSomething2
                {
                    UnknownBool = reader.ReadBoolean(),
                    UnknownPos = reader.ReadVector3()
                };
            }

            _unknown4 = reader.ReadInt32();
        }

        private struct DozerSomething1
        {
            public uint ObjectId;
            public int Unknown;
        }

        private struct DozerSomething2
        {
            public bool UnknownBool;
            public Vector3 UnknownPos;
        }
    }

    /// <summary>
    /// Allows the use of VoiceRepair, VoiceBuildResponse, VoiceNoBuild and VoiceTaskComplete 
    /// within UnitSpecificSounds section of the object.
    /// Requires Kindof = DOZER.
    /// </summary>
    public sealed class DozerAIUpdateModuleData : AIUpdateModuleData
    {
        internal new static DozerAIUpdateModuleData Parse(IniParser parser) => parser.ParseBlock(FieldParseTable);

        private new static readonly IniParseTable<DozerAIUpdateModuleData> FieldParseTable = AIUpdateModuleData.FieldParseTable
            .Concat(new IniParseTable<DozerAIUpdateModuleData>
            {
                { "RepairHealthPercentPerSecond", (parser, x) => x.RepairHealthPercentPerSecond = parser.ParsePercentage() },
                { "BoredTime", (parser, x) => x.BoredTime = parser.ParseInteger() },
                { "BoredRange", (parser, x) => x.BoredRange = parser.ParseInteger() },
            });

        public Percentage RepairHealthPercentPerSecond { get; private set; }
        public int BoredTime { get; private set; }
        public int BoredRange { get; private set; }

        internal override AIUpdate CreateAIUpdate(GameObject gameObject)
        {
            return new DozerAIUpdate(gameObject, this);
        }
    }
}
