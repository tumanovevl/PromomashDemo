export interface CreateUserCommand {
    login: string | undefined;
    password: string | undefined;
    countryId: number;
    provinceId: number;
}