import {Component, OnInit, ViewChild} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, NgForm, ValidationErrors, ValidatorFn, Validators} from '@angular/forms';
import { CountriesService } from 'src/app/shared/api/catalogs/counties/countries.service';
import { CountryLookupDto } from 'src/app/shared/api/catalogs/counties/models/countryLookupDto';
import { GetFilteredCountryListQuery } from 'src/app/shared/api/catalogs/counties/models/getFilteredCountryListQuery';
import { GetFilteredProvinceListQuery } from 'src/app/shared/api/catalogs/provinces/models/getFilteredProvinceListQuery';
import { ProvinceLookupDto } from 'src/app/shared/api/catalogs/provinces/models/provinceLookupDto';
import { ProvincesService } from 'src/app/shared/api/catalogs/provinces/provinces.service';
import { CreateUserCommand } from 'src/app/shared/api/user-management/users/models/createUserCommand';
import { UsersService } from 'src/app/shared/api/user-management/users/users.service';
import {MatSnackBar, MatSnackBarConfig} from '@angular/material/snack-bar';
import { MatStepper } from '@angular/material/stepper';

@Component({
  selector: 'user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css'],
  providers: [UsersService, CountriesService, ProvincesService]
})
export class UserRegistrationComponent implements OnInit {
  @ViewChild('stepper') stepper: MatStepper;
  @ViewChild('firstStepFormDirective') private firstStepFormDirective: NgForm;
  @ViewChild('secondStepFormDirective') private secondStepFormDirective: NgForm;
  
  firstStepFormGroup: FormGroup;
  secondStepFormGroup: FormGroup;
  countriesList: CountryLookupDto[] | undefined;
  provincesList: ProvinceLookupDto[] | undefined;
  selectedProvinceId: number | null;

  private _provinceFilterQuery = {} as GetFilteredProvinceListQuery;
  private _countryFilterQuery = {} as GetFilteredCountryListQuery;
  private _snackBarConfig: MatSnackBarConfig = { duration: 2000, verticalPosition: 'top'};

  get login() { return this.firstStepFormGroup.get('login'); }
  get password() { return this.firstStepFormGroup.get('password'); }
  get confirmPassword() { return this.firstStepFormGroup.get('confirmPassword'); }
  get agreeRegistration() { return this.firstStepFormGroup.get('agreeRegistration'); }
  get country() { return this.secondStepFormGroup.get('country'); }  
  get province() { return this.secondStepFormGroup.get('province'); }

  constructor(
    private _formBuilder: FormBuilder,
    private _usersService: UsersService,
    private _countriesService: CountriesService,
    private _provincesService: ProvincesService,
    private _snackBar: MatSnackBar
    ) {}

  ngOnInit() {
    this.initFormGroups();

    this.getCountries();

    this.country?.valueChanges.subscribe(v => {
      this._provinceFilterQuery.countryId = v;
      this.selectedProvinceId = null;

      this.getProvinces();
    });
  }

  initFormGroups(){
    this.firstStepFormGroup = this._formBuilder.group({
      login: ['',Validators.compose([Validators.required, Validators.email])],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      agreeRegistration: ['', Validators.requiredTrue],
    }, { validators: [this.passwordMatchValidator, this.passwordStrongFormatValidator]});
  
    this.secondStepFormGroup = this._formBuilder.group({
      country: ['', Validators.required],
      countryFilter: [''],
      province: ['', Validators.required],
      provinceFilter: [''],
    });
  }

  onPasswordInput() {
    if (this.firstStepFormGroup.hasError('passwordBadFormat'))
      this.password?.setErrors([{'passwordBadFormat': true}]);
    else
      this.password?.setErrors(null);
  }

  onPasswordConfirmationInput() {
    if (this.firstStepFormGroup.hasError('passwordMismatch'))
      this.confirmPassword?.setErrors([{'passwordMismatch': true}]);
    else
      this.confirmPassword?.setErrors(null);
  }

  passwordMatchValidator: ValidatorFn = (formGroup: AbstractControl):  ValidationErrors | null => { 
    const password = formGroup.get('password')?.value;
    const confirmPassword = formGroup.get('confirmPassword')?.value

    return password === confirmPassword ? null : { passwordMismatch: true }
  }

  passwordStrongFormatValidator: ValidatorFn = (formGroup: AbstractControl):  ValidationErrors | null => { 
    const password: string = formGroup.get('password')?.value;

    return /.*[A-Z].*/.test(password) &&  /.*[0-9].*/.test(password) ? null : { passwordBadFormat: true }
  }

  getCountries(){
    this._countriesService.GetFilteredCountryList(this._countryFilterQuery)
    .subscribe(res => {
      this.countriesList = res.pagedList.items;
    }, error => {
      console.log(error);
    })
  }

  getProvinces(){
    this._provincesService.GetFilteredProvinceList(this._provinceFilterQuery)
    .subscribe(res => {
      this.provincesList = res.pagedList.items;
      
    }, error => {
      console.log(error);
    })
  }

  save() {
    const createCommand: CreateUserCommand = {
      login: this.login?.value,
      password: this.password?.value,
      countryId: this.country?.value,
      provinceId: this.province?.value
    };

    this._usersService.create(createCommand)
      .subscribe((res) => {
        this._snackBar.open('Registration successfully completed!', undefined, this._snackBarConfig);

        this.stepper.reset();
        this.firstStepFormDirective.resetForm();
        this.secondStepFormDirective.resetForm();
      }, error => {
        this._snackBar.open('An error occurred during registration, please contact the administrator', undefined, this._snackBarConfig);

        console.log(error);
      });
  }
}