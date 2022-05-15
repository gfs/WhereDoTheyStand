using System.Text.Json;
using System.Text.Json.Serialization;

namespace WhereTheyStand.Lib
{
    public record CandidateId
    {
        [JsonConstructor]
        public CandidateId(string id)
        {
            this.id = id;
        }
        public string id { get; set; }
    }
    public record OrganizationId(string id);
    public record DonationId(string id);

    public class CandidateIdJsonConverter : JsonConverter<CandidateId>
    {
        public override CandidateId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new CandidateId(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, CandidateId value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
