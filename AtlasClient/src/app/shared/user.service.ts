import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, NgForm } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IssuesModel } from './issues-model.model';
import { Router, ActivatedRoute, RouterStateSnapshot, Resolve, ActivatedRouteSnapshot } from '@angular/router';
import * as jwt_decode from 'jwt-decode';
import { ToastrService } from 'ngx-toastr';
import { VersionModel } from './version-model.model';
import { ShareModel } from './share-model.model';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Observable } from 'rxjs/internal/Observable';
import { tap, take } from 'rxjs/operators';
import { SearchModel } from './search-model.model';
import { ProjectModel } from './project-model.model';
import { CommentsModel } from './comments-model.model';
import { ActivityModel } from './activity-model.model';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    config: any;
    formData: IssuesModel;
    projectData: ProjectModel;
    searchFilter: SearchModel;
    commentsData: CommentsModel;
    activityData: ActivityModel;
    initialActivityData: ActivityModel;

    readonly BaseURL = 'http://localhost:1531/api';
    list: IssuesModel[];
    projectList: ProjectModel[];
    sharedList: ShareModel[];
    VersionList: VersionModel[];
    commentsList: CommentsModel[];
    activityList: ActivityModel[];

    userIdString: string;
    userName: string;
    Email: string;

    search: string;

    rawContent: string;
    codec: string;

    count: number;
    Pages: number;
    curentPage = 1;
    countPerPage = 10;
    totalIssuesCount: number;
    min = 60000;
    delay = 180;

    forceHighlight: string;

    ShareFileCount: boolean;

    VersionTag: string;
    AssigneedProject: string;

    IssueID: string;
    FileInfo: IssuesModel;
    FileCount: boolean;
    ShareCount: boolean;
    ExpDate: string;

    local = 'http://localhost:4200/';

    UpdateContentVerificator: IssuesModel;
    sharedFormData: ShareModel;
    Version: VersionModel;

    Auth: string;

    loggedUserName: string;
    loggedFirstName: string;
    loggedLastName: string;
    loggedRank: string;

    constructor(
        private fb: FormBuilder,
        private http: HttpClient,
        private router: Router,
        private toastr: ToastrService,

    ) {
        ;
        this.config = {
            itemsPerPage: 10,
            currentPage: 1,
            totalItems: 0
        }
    }

    getFileId() {
        this.IssueID = this.formData.IssueID;
    }

    //REGISTER
    formModel = this.fb.group({
        FirstName: ['', [Validators.required, Validators.minLength(2)]],
        LastName: ['', [Validators.required, Validators.minLength(2)]],
        UserName: ['', [Validators.required, Validators.minLength(6), Validators.pattern("^[a-zA-Z][a-zA-Z0-9._]*[a-zA-Z0-9]$")]],
        Email: ['', [Validators.email, Validators.required, Validators.pattern("^[a-zA-Z0-9._@]*$")]],
        PhoneNumber: ['', [Validators.required, Validators.minLength(10), Validators.pattern("^[0-9]*$"), Validators.maxLength(14)]],
        Passwords: this.fb.group({
            Password: ['', [Validators.required, Validators.minLength(6)]],
            ConfirmPassword: ['', Validators.required]
        }, { validator: this.comparePasswords }),
        AssigneedProject: ['',],
        Rank: ['', [Validators.required, Validators.minLength(2)]]

    });
    updateModel = this.fb.group({
        FirstName: ['', [Validators.required, Validators.minLength(2)]],
        LastName: ['', [Validators.required, Validators.minLength(2)]],
        UserName: ['', [Validators.required, Validators.minLength(6), Validators.pattern("^[a-zA-Z][a-zA-Z0-9._]*[a-zA-Z0-9]$")]],
        Email: ['', [Validators.email, Validators.required, Validators.pattern("^[a-zA-Z0-9._@]*$")]],
        PhoneNumber: ['', [Validators.required, Validators.minLength(10), Validators.pattern("^[0-9]*$"), Validators.maxLength(14)]],
    });

    passwordsModel = this.fb.group({
        Passwords: this.fb.group({
            Password: ['', [Validators.required, Validators.minLength(6)]],
            ConfirmPassword: ['', Validators.required]
        }, { validator: this.comparePasswords })
    });

    commentsModel = this.fb.group({


    })

    getDateTime() {
        var today = new Date();
        var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
        var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
        var dateTime = date + ' ' + time;

        return dateTime;
    }

    dateAdd() {

        var date = new Date();
        var min = 60000;
        var delay = 180;

        date.setFullYear(date.getFullYear());
        date.getMonth() + 1;
        date.getDate();
        date.getTime();
        date.getMinutes()
        date.getSeconds();

        var ret = new Date(date);
        ret.setTime(ret.getTime() + (delay * min));
        var checkRollover = function () { if (ret.getDate() != date.getDate()) ret.setDate(0); };
        switch (String(this.ExpDate).toLowerCase()) {
            case '': ret = undefined; break;
            case 'never': ret = undefined; break;
            case '1year': ret.setFullYear(ret.getFullYear() + 1); checkRollover(); break;
            case '6months': ret.setMonth(ret.getMonth() + 6); checkRollover(); break;
            case '1month': ret.setMonth(ret.getMonth() + 1); checkRollover(); break;
            case '2weeks': ret.setDate(ret.getDate() + 14); break;
            case '1week': ret.setDate(ret.getDate() + 7); break;
            case '3days': ret.setDate(ret.getDate() + 3); break;
            case '1day': ret.setDate(ret.getDate() + 1); break;
            case '6hours': ret.setTime(ret.getTime() + (360 * min)); break;
            case '30min': ret.setTime(ret.getTime() + (30 * min)); break;
            default: ret = undefined; break;
        }
        return ret;
    }

    resetForm() {
        this.formData = {
            IssueID: '',
            Project: '',
            IssueType: '',
            IssueSummary: '',
            IssueDescription: '',
            IssueStatus: '',
            IssueResolution: '',
            IssueStepsToRepro: '',
            IssueSeverity: '',
            IssueProbability: '',
            IssueCreatedDate: new Date(),
            IssueModifiedDate: new Date(),
            IssueFixVersion: '',
            IssueReporter: '',
            IssueAssignee: ''
            // UserId: this.getTokenUserId()
        }
    }

    resetProjectForm() {
        this.projectData = {
            ProjectKEY: '',
            ProjectText: '',
            ProjectTime: new Date(),
        }
    }

    comparePasswords(fb: FormGroup) {
        let confirmPasswordControl = fb.get('ConfirmPassword');

        if (confirmPasswordControl.errors == null || 'passwordMismatch' in confirmPasswordControl.errors) {
            if (fb.get('Password').value != confirmPasswordControl.value)
                confirmPasswordControl.setErrors({ passwordMismatch: true });
            else
                confirmPasswordControl.setErrors(null);

        }
    }

    register() {
        var body = {

            FirstName: this.formModel.value.FirstName,
            LastName: this.formModel.value.LastName,
            UserName: this.formModel.value.UserName,
            Email: this.formModel.value.Email,
            PhoneNumber: this.formModel.value.PhoneNumber,
            Password: this.formModel.value.Passwords.Password,
            AssigneedProject: this.formModel.value.AssigneedProject,
            Rank: this.formModel.value.Rank,
            Access: "10"

        };
        return this.http.post(this.BaseURL + '/ApplicationUser/Register', body);
    }

    updateDetails() {
        var update = {

            FirstName: this.updateModel.value.FirstName,
            LastName: this.updateModel.value.LastName,
            UserName: this.updateModel.value.UserName,
            Email: this.updateModel.value.Email,
            PhoneNumber: this.updateModel.value.PhoneNumber,

        };
        return this.http.put(this.BaseURL + '/ApplicationUser/UpdateDetails', update);
    }

    updatePassword() {
        var update = {

            Password: this.passwordsModel.value.Passwords.Password

        };
        return this.http.put(this.BaseURL + '/ApplicationUser/UpdatePassword', update);
    }

    login(form) {
        return this.http.post(this.BaseURL + '/ApplicationUser/Login', form);
    }

    getUserProfile() {
        return this.http.get(this.BaseURL + '/UserProfile');
    }

    getUserId() {
        var token = localStorage.getItem('token');
        var decoded = jwt_decode(token);
        this.userIdString = decoded['UserID'];

    }

    getloggedUserName() {
        var token = localStorage.getItem('token');
        var decoded = jwt_decode(token);
        this.loggedUserName = decoded['UserName'];
    }

    getloggedFirstName() {
        var token = localStorage.getItem('token');
        var decoded = jwt_decode(token);
        this.loggedFirstName = decoded['FirstName'];
    }

    getloggedLastName() {
        var token = localStorage.getItem('token');
        var decoded = jwt_decode(token);
        this.loggedLastName = decoded['LastName'];
    }

    getloggedRank() {
        var token = localStorage.getItem('token');
        var decoded = jwt_decode(token);
        this.loggedRank = decoded['Rank'];
    }

    getAssigneedProjectFromToken() {
        var token = localStorage.getItem('token');
        var decoded = jwt_decode(token);
        this.AssigneedProject = decoded['AssigneedProject']
    }
    getTokenUserId() {
        var token = localStorage.getItem('token');
        var decoded = jwt_decode(token);

        return decoded['UserID'];
    }

    postFile() {
        return this.http.post(this.BaseURL + '/Issues', this.formData);
    }

    postComment() {
        return this.http.post(this.BaseURL + '/Comments', this.commentsData);
    }

    postProject() {
        return this.http.post(this.BaseURL + '/Projects', this.projectData);
    }

    postActivity() {
        this.prepareActivityData();
        return this.http.post(this.BaseURL + '/Activity', this.activityData);
    }

    putFile() {
        this.formData.IssueModifiedDate = new Date();
        return this.http.put(this.BaseURL + '/Issues/' + this.formData.IssueID, this.formData);
    }

    editProject() {
        return this.http.put(this.BaseURL + '/Projects/' + this.projectData.ProjectKEY, this.projectData);
    }

    deleteFile(id) {
        return this.http.delete(this.BaseURL + '/Issues/' + id);
    }

    deleteActivity() {
        return this.http.delete(this.BaseURL + '/Activity/' + this.formData.IssueID);
    }

    refreshList() {
        this.getAssigneedProjectFromToken();
        this.http.get(this.BaseURL + '/Issues?skip=' + ((this.curentPage - 1) * this.countPerPage) + '&take=' + this.countPerPage + '&project=' + this.AssigneedProject)
            .toPromise()
            .then(res => this.list = res as IssuesModel[]);
        this.getPaginationCount();
    }

    getCommentsList() {
        this.http.get(this.BaseURL + '/Comments?issuekey=' + this.formData.IssueID)
            .toPromise()
            .then(res => this.commentsList = res as CommentsModel[]);
    }

    getActivityList() {
        this.http.get(this.BaseURL + '/Activity?issuekey=' + this.formData.IssueID)
            .toPromise()
            .then(res => this.activityList = res as ActivityModel[]);
    }

    getProjectsList() {
        this.http.get(this.BaseURL + '/Projects')
            .toPromise()
            .then(res => this.projectList = res as ProjectModel[]);
    }

    refreshVersionList() {
        this.http.get(this.BaseURL + "/Versions?version=" + this.formData.IssueID)
            .toPromise()
            .then(res =>
                this.VersionList = res as VersionModel[]);
    }

    getPaginationCount() {
        this.http.get<number>(this.BaseURL + '/Count').subscribe(result => {
            this.config.totalItems = result;
        });
    }

    getFileCount() {
        return this.http.get<boolean>(this.BaseURL + '/Count/File?Name=' + this.formData.IssueSummary);
    }

    getTypeIssuesCount() {
        return this.http.get<number>(this.BaseURL + '/Count/Type?filetype=' + this.formData.IssueType + '&project=' + this.AssigneedProject)
    }

    getVersionCount() {
        return this.http.get<number>(this.BaseURL + '/Count/CountVersion?fileid=' + this.formData.IssueID);
    }

    reportedByMeFilter() {
        this.getAssigneedProjectFromToken();
        this.getloggedUserName();
        this.http.get(this.BaseURL + '/Issues/Reporter?skip=' + ((this.curentPage - 1) * this.countPerPage) + '&take=' + this.countPerPage + '&reporter=' + this.loggedUserName + '&project=' + this.AssigneedProject)
            .toPromise()
            .then(res => this.list = res as IssuesModel[]);
        this.getPaginationCount();
    }

    assignedToMeFilter() {
        this.getAssigneedProjectFromToken();
        this.getloggedUserName();
        this.http.get(this.BaseURL + '/Issues/Assignee?skip=' + ((this.curentPage - 1) * this.countPerPage) + '&take=' + this.countPerPage + '&assignee=' + this.loggedUserName + '&project=' + this.AssigneedProject)
            .toPromise()
            .then(res => this.list = res as IssuesModel[]);
        this.getPaginationCount();
    }

    //CONDITION
    numberOnly(event): boolean {
        const charCode = (event.which) ? event.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }

    onLogout() {
        localStorage.removeItem('token');
        this.router.navigate(['/user/login']);
    }

    insertRecord(form: NgForm) {
        if (this.userIdString != this.getTokenUserId()) {
            localStorage.removeItem('token');
            this.router.navigate(['/user/login']);
        }
        else {
            this.getAssigneedProjectFromToken();
            this.getloggedUserName();
            this.formData.Project = this.AssigneedProject;
            this.formData.IssueReporter = this.loggedUserName;
            this.formData.IssueStatus = "Open";
            this.formData.IssueResolution = "Unresolved";
            this.formData.IssueFixVersion = "";
            this.getTypeIssuesCount().subscribe(result => {
                this.totalIssuesCount = result;
                console.log(this.formData);
                this.formData.IssueID = this.AssigneedProject + '-' + this.formData.IssueType + '-' + (this.totalIssuesCount + 1).toString();
                this.postFile().subscribe(
                    res => {
                        this.toastr.success('Submitted successfully', 'File Creation Complete');
                        this.refreshList();
                    },
                    (err) => {
                        console.log(err);
                    }
                );
            }
            );
        }
    }

    insertProject(form: NgForm) {
        if (this.userIdString != this.getTokenUserId()) {
            localStorage.removeItem('token');
            this.router.navigate(['/user/login']);
        }
        else {
            /*this.getFileCount().subscribe(result => {
                this.FileCount = result;
                if (this.FileCount == true) {
                    this.toastr.warning('Submitted failed', 'There is another file with the name: "' + this.formData.IssueSummary + '"');
                }
                else {*/
            console.log(this.projectData);
            this.postProject().subscribe(
                res => {
                    this.toastr.success('Submitted successfully', 'File Creation Complete');
                    this.refreshList();
                },
                (err) => {
                    console.log(err);
                }
            );
        }
        //});
        //}
    }

    insertComment(form: NgForm) {
        this.postComment().subscribe(res => {
            this.toastr.success('Comment added successfully');
            this.getCommentsList();
        },
            (err) => {
                console.log(err);
            }
        );

    }

    updateProject() {
        /*if (this.userIdString != this.getTokenUserId()) {
            localStorage.removeItem('token');
            this.router.navigate(['/user/login']);
            document.querySelector(".modal-backdrop.fade.show").remove();
        }
        else {*/
        this.editProject().subscribe(
            res => {
                this.toastr.info('Project Update Completed');
                //this.revision();
                this.getProjectsList();
            },
            err => {
                console.log(err);
            }
        );
        /*this.getVersionCount().subscribe(
            res => {
                this.VersionTag = 'Version ' + (res + 1).toString();
                this.insertVersion();
            }*/
        //TO REPAIR
        //this.sharePut();

        //}
    }



    updateRecord() {
        /*if (this.userIdString != this.getTokenUserId()) {
            localStorage.removeItem('token');
            this.router.navigate(['/user/login']);
            document.querySelector(".modal-backdrop.fade.show").remove();
        }
        else {*/
        console.log(this.UpdateContentVerificator);
        console.log(this.Version);
        this.putFile().subscribe(
            res => {
                this.toastr.info('File Update Completed', 'Versions Has Been Updated');
                this.revision();
                this.refreshList();
            },
            err => {
                console.log(err);
            }
        );
        this.getVersionCount().subscribe(
            res => {
                this.VersionTag = 'Version ' + (res + 1).toString();
                this.insertVersion();
            });
        this.postActivity().subscribe();
    }

    initializeComment() {
        this.getloggedUserName();
        this.commentsData = {
            CommentID: 0,
            CommentText: '',
            CommentUser: this.loggedUserName,
            CommentTime: new Date(),
            IssueID: this.formData.IssueID
        }
    }


    revision() {
        this.UpdateContentVerificator = {
            IssueID: this.formData.IssueID,
            Project: this.formData.Project,
            IssueType: this.formData.IssueType,
            IssueSummary: this.formData.IssueSummary,
            IssueDescription: this.formData.IssueDescription,
            IssueStatus: this.formData.IssueStatus,
            IssueResolution: this.formData.IssueResolution,
            IssueStepsToRepro: this.formData.IssueStepsToRepro,
            IssueSeverity: this.formData.IssueSeverity,
            IssueProbability: this.formData.IssueProbability,
            IssueCreatedDate: this.formData.IssueCreatedDate,
            IssueModifiedDate: new Date(),
            IssueFixVersion: this.formData.IssueFixVersion,
            IssueReporter: this.formData.IssueReporter,
            IssueAssignee: this.formData.IssueAssignee
        }
    }

    init() {
        this.sharedFormData = {
            Id: 1,
            Name: '',
            Description: '',
            Syntax: '',
            IdentityString: '',
            LastModified: '',
            Content: '',
            FileId: 1
        }
    }

    insertVersion() {
        this.getloggedUserName();
        this.Version = {
            VersionID: 0,
            VersionTag: this.VersionTag,
            Project: this.UpdateContentVerificator.Project,
            IssueType: this.UpdateContentVerificator.IssueType,
            IssueSummary: this.UpdateContentVerificator.IssueSummary,
            IssueDescription: this.UpdateContentVerificator.IssueDescription,
            IssueStatus: this.formData.IssueStatus,
            IssueResolution: this.formData.IssueResolution,
            IssueStepsToRepro: this.UpdateContentVerificator.IssueStepsToRepro,
            IssueSeverity: this.UpdateContentVerificator.IssueSeverity,
            IssueProbability: this.UpdateContentVerificator.IssueProbability,
            IssueCreatedDate: this.UpdateContentVerificator.IssueCreatedDate,
            IssueModifiedDate: this.UpdateContentVerificator.IssueModifiedDate,
            ModifiedBy: this.loggedUserName,
            IssueFixVersion: this.UpdateContentVerificator.IssueFixVersion,
            IssueReporter: this.UpdateContentVerificator.IssueReporter,
            IssueAssignee: this.UpdateContentVerificator.IssueAssignee,
            IssueID: this.UpdateContentVerificator.IssueID,

        }
        return this.http.post(this.BaseURL + '/Versions/', this.Version).subscribe(
            res => {
                this.refreshVersionList();
            },
            (err) => {
                console.log(err);
            }
        );
    }

    onRedirect() {
        window.location.href = this.local + 'edit/' + this.formData.IssueID;
    }

    onLoginRedirect() {
        this.router.navigate(['user/login']);
    }

    onRegisterRedirect() {
        this.router.navigate(['user/registration']);
    }

    getVersions() {
        window.location.href = this.local + 'version-view/' + this.formData.IssueID;
    }

    onDelete(IssueID) {
        if (this.userIdString != this.getTokenUserId()) {
            localStorage.removeItem('token');
            this.router.navigate(['/user/login']);
            document.querySelector(".modal-backdrop.fade.show").remove();
        }
        else {
            if (confirm('Are you sure to delete this record ?')) {
                this.deleteFile(IssueID)
                    .subscribe(res => {
                        this.refreshList();
                        this.toastr.warning('Submitted successfully', 'File Delete Complete');
                    },
                        err => {
                            console.log(err);
                        });

            }
        }
    }
    searchAction() {
        this.getAssigneedProjectFromToken();
        var inputValue = (<HTMLInputElement>document.getElementById("Search")).value;
        if (inputValue == "") {
            this.refreshList();
        }
        else {
            this.config.totalItems = 1;
            return this.http.get(this.BaseURL + '/Issues/Search?search=' + inputValue + '&project=' + this.AssigneedProject).toPromise()
                .then(res => this.list = res as IssuesModel[]);
        }
    }

    prepareActivityData() {
        this.getloggedUserName();
        this.activityData = {
            ActivityID: 0,
            ActivityText: '',
            ProjectOldValue: this.initialActivityData.ProjectOldValue,
            ProjectNewValue: this.formData.Project,
            IssueTypeOldValue: this.initialActivityData.IssueTypeOldValue,
            IssueTypeNewValue: this.formData.IssueType,
            IssueSummaryOldValue: this.initialActivityData.IssueSummaryOldValue,
            IssueSummaryNewValue: this.formData.IssueSummary,
            IssueDescriptionOldValue: this.initialActivityData.IssueDescriptionOldValue,
            IssueDescriptionNewValue: this.formData.IssueDescription,
            IssueStatusOldValue: this.initialActivityData.IssueStatusOldValue,
            IssueStatusNewValue: this.formData.IssueStatus,
            IssueResolutionOldValue: this.initialActivityData.IssueResolutionOldValue,
            IssueResolutionNewValue: this.formData.IssueResolution,
            IssueStepsToReproOldValue: this.initialActivityData.IssueStepsToReproOldValue,
            IssueStepsToReproNewValue: this.formData.IssueStepsToRepro,
            IssueSeverityOldValue: this.initialActivityData.IssueSeverityOldValue,
            IssueSeverityNewValue: this.formData.IssueSeverity,
            IssueProbabilityOldValue: this.initialActivityData.IssueProbabilityOldValue,
            IssueProbabilityNewValue: this.formData.IssueProbability,
            IssueFixVersionOldValue: this.initialActivityData.IssueFixVersionOldValue,
            IssueFixVersionNewValue: this.formData.IssueFixVersion,
            IssueReporterOldValue: this.initialActivityData.IssueReporterOldValue,
            IssueReporterNewValue: this.formData.IssueReporter,
            IssueAssigneeOldValue: this.initialActivityData.IssueAssigneeOldValue,
            IssueAssigneeNewValue: this.formData.IssueAssignee,
            ActivityUser: this.loggedUserName,
            ActivityTime: new Date,
            IssueID: this.formData.IssueID,
        }
    }

    restoreVersion() {

        this.formData = {
            IssueID: this.Version.IssueID,
            Project: this.Version.Project,
            IssueType: this.Version.IssueType,
            IssueSummary: this.Version.IssueSummary,
            IssueDescription: this.Version.IssueDescription,
            IssueStatus: this.Version.IssueStatus,
            IssueResolution: this.Version.IssueResolution,
            IssueStepsToRepro: this.Version.IssueStepsToRepro,
            IssueSeverity: this.Version.IssueSeverity,
            IssueProbability: this.Version.IssueProbability,
            IssueCreatedDate: this.Version.IssueCreatedDate,
            IssueModifiedDate: this.Version.IssueModifiedDate,
            IssueFixVersion: this.Version.IssueFixVersion,
            IssueReporter: this.Version.IssueReporter,
            IssueAssignee: this.Version.IssueAssignee,
        }
        console.log(this.formData);
        this.revision();
        this.updateRecord();
    }
    storeOldActivity() {
        this.initialActivityData = {
          ActivityID: 0,
          ActivityText: '',
          ProjectOldValue: this.formData.Project,
          ProjectNewValue: '',
          IssueTypeOldValue: this.formData.IssueType,
          IssueTypeNewValue: '',
          IssueSummaryOldValue: this.formData.IssueSummary,
          IssueSummaryNewValue: '',
          IssueDescriptionOldValue: this.formData.IssueDescription,
          IssueDescriptionNewValue: '',
          IssueStatusOldValue: this.formData.IssueStatus,
          IssueStatusNewValue: '',
          IssueResolutionOldValue: this.formData.IssueResolution,
          IssueResolutionNewValue: '',
          IssueStepsToReproOldValue: this.formData.IssueStepsToRepro,
          IssueStepsToReproNewValue: '',
          IssueSeverityOldValue: this.formData.IssueSeverity,
          IssueSeverityNewValue: '',
          IssueProbabilityOldValue: this.formData.IssueProbability,
          IssueProbabilityNewValue: '',
          IssueFixVersionOldValue: this.formData.IssueFixVersion,
          IssueFixVersionNewValue: '',
          IssueReporterOldValue: this.formData.IssueReporter,
          IssueReporterNewValue: '',
          IssueAssigneeOldValue: this.formData.IssueAssignee,
          IssueAssigneeNewValue: '',
          ActivityUser: this.loggedUserName,
          ActivityTime: new Date,
          IssueID: this.formData.IssueID,
        }
      }
}
