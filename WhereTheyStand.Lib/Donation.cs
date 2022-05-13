namespace WhereTheyStand.Lib;

public record Donation(Candidate Candidate, Organization org, int Total, int Pacs, int Indivs, DateTime Releasedate, int Rank);