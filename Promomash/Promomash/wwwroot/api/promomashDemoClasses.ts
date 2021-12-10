/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming



export interface CountryListVm {
    pagedList: PagedListOfCountryLookupDto | undefined;
}

export interface PagedListBase {
    currentPage: number;
    pageCount: number;
    pageSize: number;
    rowCount: number;
    firstRowOnPage: number;
    lastRowOnPage: number;
}

export interface PagedListOfCountryLookupDto extends PagedListBase {
    items: CountryLookupDto[] | undefined;
}

export interface CountryLookupDto {
    id: number;
    title: string | undefined;
}

export interface GetFilteredCountryListQuery {
    title: string | undefined;
    page: number;
    pageSize: number;
}

export interface ProvinceListVm {
    pagedList: PagedListOfProvinceLookupDto | undefined;
}

export interface PagedListOfProvinceLookupDto extends PagedListBase {
    items: ProvinceLookupDto[] | undefined;
}

export interface ProvinceLookupDto {
    id: number;
    title: string | undefined;
}

export interface GetFilteredProvinceListQuery {
    title: string | undefined;
    countryId: number | undefined;
    page: number;
    pageSize: number;
}

export interface UserVm {
    id: number;
    login: string | undefined;
    countryTitle: string | undefined;
    provinceTitle: string | undefined;
}

export interface ProblemDetails {
    type: string | undefined;
    title: string | undefined;
    status: number | undefined;
    detail: string | undefined;
    instance: string | undefined;
    extensions: { [key: string]: any; } | undefined;
}

export interface CreateUserCommand {
    login: string | undefined;
    password: string | undefined;
    countryId: number;
    provinceId: number;
}