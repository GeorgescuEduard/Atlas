import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import hljs from 'highlight.js';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { VersionModel } from '../../shared/version-model.model';
import { IssuesModel } from 'src/app/shared/issues-model.model';

@Component({
  selector: 'app-view-selected-version',
  templateUrl: './view-selected-version.component.html',
  styles: []
})
export class ViewSelectedVersionComponent implements OnInit {
  config: any;
  versionData: VersionModel;
  language:string;

  constructor(
    public service: UserService,
    private http: HttpClient,
    private router: Router,
    private route: ActivatedRoute,

  ) {}

  ngOnInit() {
    this.route.data.subscribe(
      (data: { file: any }) => {
          this.versionData = data.file as VersionModel;
          this.service.Version = this.versionData;
          this.getFile();
        });
  }

 populateForm(version: VersionModel) {
    this.service.Version = Object.assign({}, version);
    
  }

  getFile() {
    return this.http.get(this.service.BaseURL + '/Versions/' + this.versionData.IssueID)
    .subscribe(res => {
      this.service.UpdateContentVerificator = res as IssuesModel;
      console.log(this.service.UpdateContentVerificator);
    });
}

}