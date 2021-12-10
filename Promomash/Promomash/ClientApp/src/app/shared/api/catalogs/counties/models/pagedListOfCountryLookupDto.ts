import { PagedListBase } from "../../../models/query/pagedListBase";
import { CountryLookupDto } from "./countryLookupDto";

export interface PagedListOfCountryLookupDto extends PagedListBase {
    items: CountryLookupDto[];
}