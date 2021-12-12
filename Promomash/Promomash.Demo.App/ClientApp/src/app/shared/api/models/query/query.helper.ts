import { Params } from '@angular/router';

export class QueryHelper {
    public static toApiQueryParamsString(query: any): string {
        const params = Object.keys(query)
            .filter(k => query[k] !== undefined)
            .map(f => this.toEncodedString(query, f))
            .filter(p => p.length > 0);
        let result = params.join('&');
        if (result) {
            result = `?${result}`;
        }
        return result;
    }

    private static toEncodedString(query: any, field: string): string {
        if (query[field] instanceof Date) {
            return `${field}=${query[field].toISOString()}`;
        } else if (Array.isArray(query[field])) {
            if (query[field].length === 0) {
                return '';
            }
            return '';//`${field}=${query[field].map(f => (f === null) ? 'null' : f).join()}`;
        } else if (typeof query[field] === 'string' || query[field] instanceof String) {
            return query[field] ? `${field}=${encodeURIComponent(query[field])}` : '';
        } else {
            return (query[field] !== null && query[field] !== undefined) ? `${field}=${query[field]}` : '';
        }
    }
}