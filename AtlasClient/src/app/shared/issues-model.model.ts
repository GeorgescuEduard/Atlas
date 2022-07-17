export class IssuesModel {
    IssueID: string;
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
    IssueFixVersion: string;
    IssueReporter: string;
    IssueAssignee: string;
}