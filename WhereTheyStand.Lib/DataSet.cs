using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WhereTheyStand.Lib
{
    public class DataSet
    {
        public DataSet()
        {
        }

        public DataSet(SerializableDataSet serializableDataSet)
        {
            CollectionDate = DateTime.Parse(serializableDataSet.CollectionDate);
            Description = serializableDataSet.Description;
            CandidateIdToDonationIds = serializableDataSet.CandidateIdToDonationIds.ToDictionary(x => new CandidateId(x.Key), y => y.Value.Select(z => new DonationId(z)).ToList());
            OrganizationIdToDonationIds = serializableDataSet.OrganizationIdToDonationIds.ToDictionary(x => new OrganizationId(x.Key), y => y.Value.Select(z => new DonationId(z)).ToList());
            CandidateIdToCandidate = serializableDataSet.CandidateIdToCandidate.ToDictionary(x => new CandidateId(x.Key), y => y.Value);
            OrganizationIdToOrganization = serializableDataSet.OrganizationIdToOrganization.ToDictionary(x => new OrganizationId(x.Key), y => y.Value);
            DonationIdToDonation = serializableDataSet.DonationIdToDonation.ToDictionary(x => new DonationId(x.Key), y => y.Value);
        }

        public DateTime CollectionDate { get; set; }
        public string Description { get; set; } = string.Empty;

        public Dictionary<CandidateId, List<DonationId>> CandidateIdToDonationIds { get; set; } = new();
        public Dictionary<OrganizationId, List<DonationId>> OrganizationIdToDonationIds { get; set; } = new();
        public Dictionary<CandidateId, Candidate> CandidateIdToCandidate { get; set; } = new();
        public Dictionary<OrganizationId, Organization> OrganizationIdToOrganization { get; set; } = new();
        public Dictionary<DonationId, Donation> DonationIdToDonation { get; set; } = new();

        public IEnumerable<Donation> CandidateIdToDonations(CandidateId candidateId) => CandidateIdToDonationIds[candidateId].Select(x => DonationIdToDonation[x]);
        public IEnumerable<Donation> OrganizationIdToDonations(OrganizationId organizationId) => OrganizationIdToDonationIds[organizationId].Select(x => DonationIdToDonation[x]);

        public SerializableDataSet ToSerializableDataSet()
        {
            return new SerializableDataSet(this);
        }
    }

    public class SerializableDataSet
    {
        public SerializableDataSet(DataSet dataSet)
        {
            CollectionDate = dataSet.CollectionDate.ToString();
            Description = dataSet.Description;
            CandidateIdToDonationIds = dataSet.CandidateIdToDonationIds.ToDictionary(x => x.Key.ToString(), y => y.Value.Select(z => z.ToString()));
            OrganizationIdToDonationIds = dataSet.OrganizationIdToDonationIds.ToDictionary(x => x.Key.ToString(), y => y.Value.Select(z => z.ToString()));
            CandidateIdToCandidate = dataSet.CandidateIdToCandidate.ToDictionary(x => x.Key.ToString(), y => y.Value);
            OrganizationIdToOrganization = dataSet.OrganizationIdToOrganization.ToDictionary(x => x.Key.ToString(), y => y.Value);
            DonationIdToDonation = dataSet.DonationIdToDonation.ToDictionary(x => x.Key.ToString(), y => y.Value);
        }

        [JsonConstructor]
        public SerializableDataSet(string CollectionDate, string Description, IDictionary<string, IEnumerable<string>> CandidateIdToDonationIds, IDictionary<string, IEnumerable<string>> OrganizationIdToDonationIds, IDictionary<string, Candidate> CandidateIdToCandidate, IDictionary<string, Organization> OrganizationIdToOrganization, IDictionary<string, Donation> DonationIdToDonation)
        {
            this.CollectionDate = CollectionDate;
            this.Description = Description;
            this.CandidateIdToDonationIds = CandidateIdToDonationIds;
            this.OrganizationIdToDonationIds = OrganizationIdToDonationIds;
            this.CandidateIdToCandidate = CandidateIdToCandidate;
            this.OrganizationIdToOrganization = OrganizationIdToOrganization;
            this.DonationIdToDonation = DonationIdToDonation;
        }

        public string CollectionDate { get; set; }
        public string Description { get; set; } = string.Empty;

        public IDictionary<string, IEnumerable<string>> CandidateIdToDonationIds { get; set; } = new Dictionary<string, IEnumerable<string>>();
        public IDictionary<string, IEnumerable<string>> OrganizationIdToDonationIds { get; set; } = new Dictionary<string, IEnumerable<string>>();
        public IDictionary<string, Candidate> CandidateIdToCandidate { get; set; } = new Dictionary<string, Candidate>();
        public IDictionary<string, Organization> OrganizationIdToOrganization { get; set; } = new Dictionary<string, Organization>();
        public IDictionary<string, Donation> DonationIdToDonation { get; set; } = new Dictionary<string, Donation>();
    }
}
