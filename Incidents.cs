﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using IncidentRecord;
//
//    var incidents = Incidents.FromJson(jsonString);

namespace IncidentRecord
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Incidents
    {
        [JsonProperty("instanceid")]
        public string Instanceid { get; set; }

        [JsonProperty("incident_no")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long IncidentNo { get; set; }

        [JsonProperty("date_reported")]
        public DateTimeOffset DateReported { get; set; }

        [JsonProperty("date_from")]
        public DateTimeOffset DateFrom { get; set; }

        [JsonProperty("date_to")]
        public DateTimeOffset DateTo { get; set; }

        [JsonProperty("ucr")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Ucr { get; set; }

        [JsonProperty("dst")]
        public string Dst { get; set; }

        [JsonProperty("beat", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Beat { get; set; }

        [JsonProperty("offense")]
        public string Offense { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("dayofweek")]
        public Dayofweek Dayofweek { get; set; }

        [JsonProperty("weapons")]
        public Weapons Weapons { get; set; }

        [JsonProperty("hour_from")]
        public string HourFrom { get; set; }

        [JsonProperty("hour_to")]
        public string HourTo { get; set; }

        [JsonProperty("address_x")]
        public string AddressX { get; set; }

        [JsonProperty("longitude_x", NullValueHandling = NullValueHandling.Ignore)]
        public string LongitudeX { get; set; }

        [JsonProperty("latitude_x", NullValueHandling = NullValueHandling.Ignore)]
        public string LatitudeX { get; set; }

        [JsonProperty("victim_age")]
        public string VictimAge { get; set; }

        [JsonProperty("suspect_age")]
        public SuspectAge SuspectAge { get; set; }

        [JsonProperty("suspect_gender", NullValueHandling = NullValueHandling.Ignore)]
        public Gender? SuspectGender { get; set; }

        [JsonProperty("ucr_group")]
        public UcrGroup UcrGroup { get; set; }

        [JsonProperty("zip")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Zip { get; set; }

        [JsonProperty("community_council_neighborhood")]
        public string CommunityCouncilNeighborhood { get; set; }

        [JsonProperty("sna_neighborhood")]
        public string SnaNeighborhood { get; set; }

        [JsonProperty("rpt_area", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? RptArea { get; set; }

        [JsonProperty("cpd_neighborhood", NullValueHandling = NullValueHandling.Ignore)]
        public string CpdNeighborhood { get; set; }

        [JsonProperty("victim_gender", NullValueHandling = NullValueHandling.Ignore)]
        public Gender? VictimGender { get; set; }

        [JsonProperty("theft_code", NullValueHandling = NullValueHandling.Ignore)]
        public TheftCode? TheftCode { get; set; }

        [JsonProperty("clsd", NullValueHandling = NullValueHandling.Ignore)]
        public Clsd? Clsd { get; set; }

        [JsonProperty("date_of_clearance", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DateOfClearance { get; set; }
    }

    public enum Clsd { FClearedByArrestAdult, HWarrantIssued, IInvestigationPending, ZEarlyClosed };

    public enum Dayofweek { Monday, Tuesday };

    public enum SuspectAge { Over70, The1825, The3140, The4150, Under18, Unknown };

    public enum Gender { Female, Male, Unknown };

    public enum TheftCode { The23FTheftFromMotorVehicle, The24OMotorVehicleTheft };

    public enum UcrGroup { BurglaryBreakingEntering, Part2Minor, Robbery, Theft };

    public enum Weapons { The12Handgun, The40PersonalWeaponsHandsFeetTeethEtc, The99None, UUnknown };

    public partial class Incidents
    {
        public static List<Incidents> FromJson(string json) => JsonConvert.DeserializeObject<List<Incidents>>(json, IncidentRecord.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<Incidents> self) => JsonConvert.SerializeObject(self, IncidentRecord.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ClsdConverter.Singleton,
                //DayofweekConverter.Singleton,
                SuspectAgeConverter.Singleton,
                GenderConverter.Singleton,
                TheftCodeConverter.Singleton,
                UcrGroupConverter.Singleton,
                WeaponsConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class ClsdConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Clsd) || t == typeof(Clsd?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "F--CLEARED BY ARREST - ADULT":
                    return Clsd.FClearedByArrestAdult;
                case "H--WARRANT ISSUED":
                    return Clsd.HWarrantIssued;
                case "I--INVESTIGATION PENDING":
                    return Clsd.IInvestigationPending;
                case "Z--EARLY CLOSED":
                    return Clsd.ZEarlyClosed;
            }
            throw new Exception("Cannot unmarshal type Clsd");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Clsd)untypedValue;
            switch (value)
            {
                case Clsd.FClearedByArrestAdult:
                    serializer.Serialize(writer, "F--CLEARED BY ARREST - ADULT");
                    return;
                case Clsd.HWarrantIssued:
                    serializer.Serialize(writer, "H--WARRANT ISSUED");
                    return;
                case Clsd.IInvestigationPending:
                    serializer.Serialize(writer, "I--INVESTIGATION PENDING");
                    return;
                case Clsd.ZEarlyClosed:
                    serializer.Serialize(writer, "Z--EARLY CLOSED");
                    return;
            }
            throw new Exception("Cannot marshal type Clsd");
        }

        public static readonly ClsdConverter Singleton = new ClsdConverter();
    }

    //internal class DayofweekConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type t) => t == typeof(Dayofweek) || t == typeof(Dayofweek?);

    //    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    //    {
    //        if (reader.TokenType == JsonToken.Null) return null;
    //        var value = serializer.Deserialize<string>(reader);
    //        switch (value)
    //        {
    //            case "MONDAY":
    //                return Dayofweek.Monday;
    //            case "TUESDAY":
    //                return Dayofweek.Tuesday;
    //        }
    //        throw new Exception("Cannot unmarshal type Dayofweek");
    //    }

    //    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    //    {
    //        if (untypedValue == null)
    //        {
    //            serializer.Serialize(writer, null);
    //            return;
    //        }
    //        var value = (Dayofweek)untypedValue;
    //        switch (value)
    //        {
    //            case Dayofweek.Monday:
    //                serializer.Serialize(writer, "MONDAY");
    //                return;
    //            case Dayofweek.Tuesday:
    //                serializer.Serialize(writer, "TUESDAY");
    //                return;
    //        }
    //        throw new Exception("Cannot marshal type Dayofweek");
    //    }

    //    public static readonly DayofweekConverter Singleton = new DayofweekConverter();
    //}

    internal class SuspectAgeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(SuspectAge) || t == typeof(SuspectAge?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "18-25":
                    return SuspectAge.The1825;
                case "31-40":
                    return SuspectAge.The3140;
                case "41-50":
                    return SuspectAge.The4150;
                case "OVER 70":
                    return SuspectAge.Over70;
                case "UNDER 18":
                    return SuspectAge.Under18;
                case "UNKNOWN":
                    return SuspectAge.Unknown;
            }
            throw new Exception("Cannot unmarshal type SuspectAge");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (SuspectAge)untypedValue;
            switch (value)
            {
                case SuspectAge.The1825:
                    serializer.Serialize(writer, "18-25");
                    return;
                case SuspectAge.The3140:
                    serializer.Serialize(writer, "31-40");
                    return;
                case SuspectAge.The4150:
                    serializer.Serialize(writer, "41-50");
                    return;
                case SuspectAge.Over70:
                    serializer.Serialize(writer, "OVER 70");
                    return;
                case SuspectAge.Under18:
                    serializer.Serialize(writer, "UNDER 18");
                    return;
                case SuspectAge.Unknown:
                    serializer.Serialize(writer, "UNKNOWN");
                    return;
            }
            throw new Exception("Cannot marshal type SuspectAge");
        }

        public static readonly SuspectAgeConverter Singleton = new SuspectAgeConverter();
    }

    internal class GenderConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Gender) || t == typeof(Gender?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "FEMALE":
                    return Gender.Female;
                case "MALE":
                    return Gender.Male;
                case "UNKNOWN":
                    return Gender.Unknown;
            }
            throw new Exception("Cannot unmarshal type Gender");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Gender)untypedValue;
            switch (value)
            {
                case Gender.Female:
                    serializer.Serialize(writer, "FEMALE");
                    return;
                case Gender.Male:
                    serializer.Serialize(writer, "MALE");
                    return;
                case Gender.Unknown:
                    serializer.Serialize(writer, "UNKNOWN");
                    return;
            }
            throw new Exception("Cannot marshal type Gender");
        }

        public static readonly GenderConverter Singleton = new GenderConverter();
    }

    internal class TheftCodeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TheftCode) || t == typeof(TheftCode?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "23F-THEFT FROM MOTOR VEHICLE":
                    return TheftCode.The23FTheftFromMotorVehicle;
                case "24O-MOTOR VEHICLE THEFT":
                    return TheftCode.The24OMotorVehicleTheft;
            }
            throw new Exception("Cannot unmarshal type TheftCode");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TheftCode)untypedValue;
            switch (value)
            {
                case TheftCode.The23FTheftFromMotorVehicle:
                    serializer.Serialize(writer, "23F-THEFT FROM MOTOR VEHICLE");
                    return;
                case TheftCode.The24OMotorVehicleTheft:
                    serializer.Serialize(writer, "24O-MOTOR VEHICLE THEFT");
                    return;
            }
            throw new Exception("Cannot marshal type TheftCode");
        }

        public static readonly TheftCodeConverter Singleton = new TheftCodeConverter();
    }

    internal class UcrGroupConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(UcrGroup) || t == typeof(UcrGroup?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "BURGLARY/BREAKING ENTERING":
                    return UcrGroup.BurglaryBreakingEntering;
                case "PART 2 MINOR":
                    return UcrGroup.Part2Minor;
                case "ROBBERY":
                    return UcrGroup.Robbery;
                case "THEFT":
                    return UcrGroup.Theft;
            }
            throw new Exception("Cannot unmarshal type UcrGroup");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (UcrGroup)untypedValue;
            switch (value)
            {
                case UcrGroup.BurglaryBreakingEntering:
                    serializer.Serialize(writer, "BURGLARY/BREAKING ENTERING");
                    return;
                case UcrGroup.Part2Minor:
                    serializer.Serialize(writer, "PART 2 MINOR");
                    return;
                case UcrGroup.Robbery:
                    serializer.Serialize(writer, "ROBBERY");
                    return;
                case UcrGroup.Theft:
                    serializer.Serialize(writer, "THEFT");
                    return;
            }
            throw new Exception("Cannot marshal type UcrGroup");
        }

        public static readonly UcrGroupConverter Singleton = new UcrGroupConverter();
    }

    internal class WeaponsConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Weapons) || t == typeof(Weapons?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "12 - HANDGUN":
                    return Weapons.The12Handgun;
                case "40 - PERSONAL WEAPONS (HANDS, FEET, TEETH, ETC.)":
                    return Weapons.The40PersonalWeaponsHandsFeetTeethEtc;
                case "99 - NONE":
                    return Weapons.The99None;
                case "U - UNKNOWN":
                    return Weapons.UUnknown;
            }
            throw new Exception("Cannot unmarshal type Weapons");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Weapons)untypedValue;
            switch (value)
            {
                case Weapons.The12Handgun:
                    serializer.Serialize(writer, "12 - HANDGUN");
                    return;
                case Weapons.The40PersonalWeaponsHandsFeetTeethEtc:
                    serializer.Serialize(writer, "40 - PERSONAL WEAPONS (HANDS, FEET, TEETH, ETC.)");
                    return;
                case Weapons.The99None:
                    serializer.Serialize(writer, "99 - NONE");
                    return;
                case Weapons.UUnknown:
                    serializer.Serialize(writer, "U - UNKNOWN");
                    return;
            }
            throw new Exception("Cannot marshal type Weapons");
        }

        public static readonly WeaponsConverter Singleton = new WeaponsConverter();
    }
}
