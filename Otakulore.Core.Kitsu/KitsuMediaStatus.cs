﻿using System.Runtime.Serialization;

namespace Otakulore.Core.Kitsu
{

    public enum KitsuMediaStatus
    {

        [EnumMember(Value = "current")] Ongoing,
        [EnumMember(Value = "finished")] Finished,
        [EnumMember(Value = "tba")] ToBeAired,
        [EnumMember(Value = "unreleased")] Unreleased,
        [EnumMember(Value = "upcoming")] Upcoming,

    }

}