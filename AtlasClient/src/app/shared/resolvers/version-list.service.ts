import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, NgForm } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router, ActivatedRoute, RouterStateSnapshot, Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { tap, take } from 'rxjs/operators';
import { FileModel } from '../file-model.model';
import { VersionModel } from '../version-model.model';
import { UserService } from '../user.service';

@Injectable({
    providedIn: 'root'
})
export class VersionListResolver implements Resolve<any> {
    readonly BaseURL = 'http://localhost:1531/api';

    local = 'http://localhost:4200/';
    versionData: VersionModel[];
    language:string;
    service: UserService;

    constructor(
        private http: HttpClient,
    ) {}
    resolve(route: ActivatedRouteSnapshot) {
        return this.http.get(this.BaseURL + '/Versions/' + route.params['version']).pipe(tap(res => { this.versionData = res as VersionModel[];}
            ));
        };
    }