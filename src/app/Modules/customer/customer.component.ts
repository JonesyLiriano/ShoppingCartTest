import { Component, OnInit } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { Customer } from 'src/app/Models/Customer';
import { CustomersService } from 'src/app/Services/Customers/customers.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css'],
})
export class CustomerComponent implements OnInit {

  registerButtonText = 'Register Customer';
  updateButtonText = 'Update Customer';
  CustomersList: Customer[];
  CustomerForm: FormGroup;
  get Customer() {
    return this.CustomerForm.get('Customers') as FormArray;
  }
  constructor(
    private fb: FormBuilder,
    private customersService: CustomersService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.CustomerForm = this.fb.group({
      Customers: this.fb.array([]),
    });
    this.GetCustomers();
  }

  AddCustomerFromGroup(): FormGroup {
    return this.fb.group({
      Id: [],
      DocumentNumber: ['', Validators.required],
      FullName: ['', Validators.required],
      CreditLimit: ['', Validators.required],
      RowVersion:[]
    });
  }

  AddFormRow() {
    this.Customer.push(this.AddCustomerFromGroup());
  }

  DeleteFormRow(index) {
    if (
      confirm(
        'Are you sure to delete the customer ' +
          this.Customer.value[index].FullName
      )
    ) {
      if (this.Customer.value[index]["Id"] != null) {
        this.customersService
          .DeleteCustomer(this.Customer.value[index]['Id'])
          .subscribe((response) => {
            console.log(response);
            this.Customer.removeAt(index);
      this.toastr.warning('Customer deleted!');
          });
      }

    }
  }

  GetCustomers() {
    this.customersService.GetCustomers().subscribe((customers) => {
      if (customers != []) {
        customers.forEach((customer: Customer) => {
          this.Customer.push(
            this.fb.group({
              Id: [customer.id],
              DocumentNumber: [customer.documentNumber, Validators.required],
              FullName: [customer.fullName, Validators.required],
              CreditLimit: [customer.creditLimit, Validators.required],
              RowVersion: [customer.rowVersion],
            })
          );
        });
      }
      this.AddFormRow();
    });
  }

  AddOrUpdateCustomer(index: number) {
    if(this.ValidateCustormerID(index)){
      if (
        confirm(
          'Are you sure to update the customer ' +
            this.Customer.value[index].FullName
        )
      ) {
        this.customersService
          .PutCustomer(this.Customer.value[index], this.Customer.value[index]["Id"])
          .subscribe((response) => {
            this.toastr.success('Customer Updated!');
            this.Customer.clear();
            this.GetCustomers();
          });
      }
    } else {

    if (
      confirm(
        'Are you sure to add the customer ' +
          this.Customer.value[index].FullName
      )
    ) {
      this.customersService
        .PostCustomer(this.Customer.value[index])
        .subscribe((response) => {
          this.toastr.success('Customer Added!');
          this.Customer.clear();
            this.GetCustomers();
        });
    }
  }
  }

  ValidateCustormerID(index) {
    if(this.Customer.value[index]["Id"] != null) {
    return true;
    }
    else
    return false;
  }
}
