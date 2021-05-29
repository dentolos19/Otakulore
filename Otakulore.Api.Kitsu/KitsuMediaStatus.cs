﻿using System.Runtime.Serialization;

namespace Otakulore.Core.Kitsu
{

    public enum KitsuMediaStatus
    {

        [EnumMember(Value = "current")] Releasing,
        [EnumMember(Value = "finished")] Finished,
        [EnumMember(Value = "tba")] Tba,
        [EnumMember(Value = "unreleased")] Unreleased,
        [EnumMember(Value = "upcoming")] Upcoming

    }

}