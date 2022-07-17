export class VersionModel {
    VersionID: number;
    VersionTag: string;
    Project: string;
    IssueType:string;
    IssueSummary:string;
    IssueDescription: string;
    IssueStatus: string;
    IssueResolution: string;
    IssueStepsToRepro: string;
    IssueSeverity:string;
    IssueProbability:string;
    IssueCreatedDate:Date;
    IssueModifiedDate: Date;
    ModifiedBy: string;
    IssueFixVersion: string;
    IssueReporter: string;
    IssueAssignee: string;
    IssueID: string;
}