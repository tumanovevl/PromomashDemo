import { PagedListBase } from "../../../models/query/pagedListBase";
import { ProvinceLookupDto } from "./provinceLookupDto";

export interface PagedListOfProvinceLookupDto extends PagedListBase {
    items: ProvinceLookupDto[];
}