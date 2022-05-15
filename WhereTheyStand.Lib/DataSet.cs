using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            CollectionDate = dataSet.CollectionDate;
            Description = dataSet.Description;
            CandidateIdToDonationIds = dataSet.CandidateIdToDonationIds.ToDictionary(x => x.Key.ToString(), y => y.Value.Select(z => z.ToString()));
            OrganizationIdToDonationIds = dataSet.OrganizationIdToDonationIds.ToDictionary(x => x.Key.ToString(), y => y.Value.Select(z => z.ToString()));
            CandidateIdToCandidate = dataSet.CandidateIdToCandidate.ToDictionary(x => x.Key.ToString(), y => y.Value);
            OrganizationIdToOrganization = dataSet.OrganizationIdToOrganization.ToDictionary(x => x.Key.ToString(), y => y.Value);
            DonationIdToDonation = dataSet.DonationIdToDonation.ToDictionary(x => x.Key.ToString(), y => y.Value);
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
            CollectionDate = dataSet.CollectionDate;
            Description = dataSet.Description;
            CandidateIdToDonationIds = dataSet.CandidateIdToDonationIds.ToDictionary(x => x.Key.ToString(), y => y.Value.Select(z => z.ToString()));
            OrganizationIdToDonationIds = dataSet.OrganizationIdToDonationIds.ToDictionary(x => x.Key.ToString(), y => y.Value.Select(z => z.ToString()));
            CandidateIdToCandidate = dataSet.CandidateIdToCandidate.ToDictionary(x => x.Key.ToString(), y => y.Value);
            OrganizationIdToOrganization = dataSet.OrganizationIdToOrganization.ToDictionary(x => x.Key.ToString(), y => y.Value);
            DonationIdToDonation = dataSet.DonationIdToDonation.ToDictionary(x => x.Key.ToString(), y => y.Value);
        }
        public DateTime CollectionDate { get; set; }
        public string Description { get; set; } = string.Empty;

        public IDictionary<string, IEnumerable<string>> CandidateIdToDonationIds { get; set; }
        public IDictionary<string, IEnumerable<string>> OrganizationIdToDonationIds { get; set; }
        public IDictionary<string, Candidate> CandidateIdToCandidate { get; set; }
        public IDictionary<string, Organization> OrganizationIdToOrganization { get; set; }
        public IDictionary<string, Donation> DonationIdToDonation { get; set; }
    }
}
