using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereTheyStand.Lib
{
    public class DataSet
    {
        public DateTime CollectionDate { get; set; }
        public string Description { get; set; } = string.Empty;

        public Dictionary<CandidateId, List<DonationId>> CandidateIdToDonationIds { get; set; } = new();
        public Dictionary<OrganizationId, List<DonationId>> OrganizationIdToDonationIds { get; set; } = new();
        public Dictionary<CandidateId, Candidate> CandidateIdToCandidate { get; set; } = new();
        public Dictionary<OrganizationId, Organization> OrganizationIdToOrganization { get; set; } = new();
        public Dictionary<DonationId, Donation> DonationIdToDonation { get; set; } = new();

        public IEnumerable<Donation> CandidateIdToDonations(CandidateId candidateId) => CandidateIdToDonationIds[candidateId].Select(x => DonationIdToDonation[x]);
        public IEnumerable<Donation> OrganizationIdToDonations(OrganizationId organizationId) => OrganizationIdToDonationIds[organizationId].Select(x => DonationIdToDonation[x]);
    }
}
