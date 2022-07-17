import { IssuesModel } from '../../shared/issues-model.model';
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserService } from 'src/app/shared/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Compiler } from '@angular/core';
import hljs from 'highlight.js';
import { VersionModel } from 'src/app/shared/version-model.model';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styles: []
})
export class ViewComponent implements OnInit {

  formData: IssuesModel;
  language: string;
  config: any;
  versionData: VersionModel[];

  constructor(
    public service: UserService,
    private route: ActivatedRoute,
    private _compiler: Compiler

  ) { }

  ngOnInit() {
    console.log('Component initialized');
    this.route.data.subscribe((data: { file: any }) => {
      this.formData = data.file as IssuesModel;
      this.service.formData = this.formData;
      this.service.revision();
      console.log(this.service.VersionList);
      this.service.commentsList = [];
      this.service.activityList = [];
      this.service.VersionList = [];
      this.service.getCommentsList();
    });
   this.service.initializeComment();
  }

  raw() {
    window.location.href = this.service.local + 'raw/' + this.formData.IssueID;
  }
  
  populateForm(version: VersionModel) {
    window.location.href = this.service.local + 'view-selected-version/' + version.VersionID;
  }
  
}