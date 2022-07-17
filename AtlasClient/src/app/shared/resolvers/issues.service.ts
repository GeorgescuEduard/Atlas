import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, NgForm } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router, ActivatedRoute, RouterStateSnapshot, Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { tap, take } from 'rxjs/operators';
import { FileModel } from '../file-model.model';
import hljs from 'highlight.js';
import { IssuesModel } from '../issues-model.model';

@Injectable({
    providedIn: 'root'
})
export class FileResolver implements Resolve<any> {
    readonly BaseURL = 'http://localhost:1531/api';

    local = 'http://localhost:4200/';
    formData: IssuesModel;
    language: string;

    constructor(
        private http: HttpClient,
    ) { }
    resolve(route: ActivatedRouteSnapshot) {
        return this.http.get(this.BaseURL + '/Issues/' + route.params['file'])
            .pipe(tap(res => {
                this.formData = res as IssuesModel;
            }
            ));
    }
}