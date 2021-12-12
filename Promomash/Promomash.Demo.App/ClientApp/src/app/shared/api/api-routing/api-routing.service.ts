import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';

@Injectable({
    providedIn: "root"
})
export class ApiRoutingService {
    getBasePath(version: number = 1): string {
        return `${environment.apiBasePath}/api`;
    }
}
