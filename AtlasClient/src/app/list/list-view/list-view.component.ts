import { IssuesModel } from '../../shared/issues-model.model';
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/user.service';
import { Router } from '@angular/router';
import hljs from 'highlight.js';
import { NgxPaginationModule } from 'ngx-pagination';
import * as $ from "jquery";
import { SearchModel } from 'src/app/shared/search-model.model';

@Component({
  selector: 'app-list-view',
  templateUrl: './list-view.component.html',
  styles: []
})
export class ListViewComponent implements OnInit {
  config: any;
  formData: IssuesModel;
  searchModel: SearchModel;


  fileId: number
  constructor(
    public service: UserService,
    private http: HttpClient,
    private router: Router,

  ) { }

  ngOnInit() {
    this.service.getUserId();
    this.service.getPaginationCount();
    this.service.refreshList();
    $("#sidebarCollapse").click(function (e) {
      e.preventDefault();
      $("#wrapper").toggleClass("toggled");
    });
  }

  populateForm(file: IssuesModel) {
    this.service.formData = Object.assign({}, file);
    console.log(this.service.formData);
  }

  pageChanged(event) {
    this.service.config.currentPage = event;
    this.service.curentPage = event;
    this.service.getPaginationCount();
    this.service.refreshList();
  }

  searchAction() {
    var inputValue = (<HTMLInputElement>document.getElementById("Search")).value;
    if (inputValue == "") {
      this.service.refreshList();
    }
    else {
      this.service.config.totalItems = 1;
      return this.http.get(this.service.BaseURL + '/File/Search?search=' + inputValue).toPromise()
        .then(res => this.service.list = res as IssuesModel[]);
    }
  }

  fileView(file: IssuesModel) {
    this.formData = Object.assign({}, file);
    window.location.href = this.service.local + 'view/' + this.formData.IssueID;
  }

  selectSyntax(file: IssuesModel) {
    this.service.formData = Object.assign({}, file);
    this.service.config.totalItems = 1;
    const encodedSyntax = encodeURIComponent(this.service.formData.IssueSeverity);
    return this.http.get(this.service.BaseURL + '/File/FindBySyntax?Syntax=' + encodedSyntax)
      .toPromise()
      .then(res => this.service.list = res as IssuesModel[]);
  }
}