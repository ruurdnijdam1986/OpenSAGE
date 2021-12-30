﻿namespace OpenSage.Logic.Object.Helpers
{
    internal sealed class ObjectFiringTrackerHelper : UpdateModule
    {
        private uint _numShotsFiredAtLastTarget;
        private uint _lastTargetObjectId;

        internal override void Load(StatePersister reader)
        {
            reader.PersistVersion(1);

            base.Load(reader);

            reader.PersistUInt32(ref _numShotsFiredAtLastTarget);
            reader.PersistObjectID(ref _lastTargetObjectId);

            reader.SkipUnknownBytes(4);
        }
    }
}
