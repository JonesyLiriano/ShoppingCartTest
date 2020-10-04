import { ShoppingCartItem } from './../../Models/ShoppingCartItem';
import { InvoiceItemsService } from './../../Services/InvoiceItems/invoice-items.service';
import { InvoiceHeaderService } from './../../Services/InvoiceHeader/invoice-header.service';
import { ShoppingCartHeader } from './../../Models/ShoppingCartHeader';
import { Product } from './../../Models/Product';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductsService } from 'src/app/Services/Products/products.service';
import { Customer } from 'src/app/Models/Customer';
import { CustomersService } from 'src/app/Services/Customers/customers.service';
import { ToastrService } from 'ngx-toastr';
import { ShoppingCartHeadersService } from 'src/app/Services/ShoppingCartHeaders/shopping-cart-headers.service';
import { ShoppingCartItemsService } from 'src/app/Services/ShoppingCartItems/shopping-cart-items.service';
import { NodeWithI18n } from '@angular/compiler';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css'],
})
export class ShoppingCartComponent implements OnInit {

  ShoppingCartItems: ShoppingCartItem[];
  orderNumber: string;
  shoppingCartTotal: number;
  customerCreditLimit: string;
  ProductsList: Product[];
  CustomersList: Customer[];
  ShoppingCartForm: FormGroup;
  get Products() {
    return this.ShoppingCartForm.get('Products') as FormArray;
  }
  constructor(
    private fb: FormBuilder,
    private productsService: ProductsService,
    private toastr: ToastrService,
    private customersService: CustomersService,
    private shoppingCartHeaderService: ShoppingCartHeadersService,
    private shoppingCartItemService: ShoppingCartItemsService,
    private invoiceHeaderService: InvoiceHeaderService,
    private invoiceItemsService: InvoiceItemsService,
    private productService: ProductsService
  ) {}

  ngOnInit(): void {
    this.ShoppingCartForm = this.fb.group({
      CustomerId: ['', Validators.required],
      CreditInvoice: [],
      Products: this.fb.array([this.AddProductFromGroup()]),
    });
    this.GetProducts();
    this.GetCustomers();
  }

  AddProductFromGroup(): FormGroup {
    return this.fb.group({
      ProductCode: ['', Validators.required],
      Description: [''],
      Price: [''],
      Available: [''],
      Qty: [null, Validators.required],
      Total: [null, Validators.required],
      RowVersion: []
    });
  }

  AddFormRow() {
    this.Products.push(this.AddProductFromGroup());
  }

  DeleteFormRow(index) {
    this.Products.removeAt(index);
    this.UpdateTotalShoppingCartValue();
  }

  GetProducts() {
    this.productsService.GetProducts().subscribe((products) => {
      this.ProductsList = products;
    });
  }

  GetCustomers() {
    this.customersService.GetCustomers().subscribe((customers) => {
      this.CustomersList = customers;
    });
  }

  ProductSelected($event, index) {
    this.Products.controls[index]
      .get('Description')
      .setValue(
        this.ProductsList.find(
          (product) => product.id == $event.value
        ).description.toString()
      );
    this.Products.controls[index]
      .get('Price')
      .setValue(
        this.ProductsList.find(
          (product) => product.id == $event.value
        ).price.toFixed(2)
      );
    this.Products.controls[index]
      .get('Available')
      .setValue(
        this.ProductsList.find(
          (product) => product.id == $event.value
        ).available.toFixed(2)
      );
  }

  CustomerSelected($event) {
    this.customerCreditLimit = this.CustomersList.find(
      (customer) => customer.id == $event.value
    ).creditLimit.toFixed(2);
  }

  UpdateTotalValue($event, index) {
    if (
      +this.Products.value[index]['Available'] <
      +this.Products.value[index]['Qty']
    ) {
      this.Products.controls[index].get('Qty').setValue(null);
      this.Products.controls[index].get('Total').setValue(null);
      this.toastr.error('Product Qty exceeds available Qty');
    } else {
      this.Products.controls[index]
        .get('Total')
        .setValue(
          (
            +this.Products.value[index]['Price'] *
            +this.Products.value[index]['Qty']
          ).toFixed(2)
        );
      this.UpdateTotalShoppingCartValue();
      this.ValidateCreditLimit();
    }
  }

  UpdateTotalShoppingCartValue() {
    this.shoppingCartTotal = 0;
    this.Products.value.forEach((product) => {
      console.log(product);
      this.shoppingCartTotal += +product['Total'];
    });
  }

  ValidateCreditLimit(): boolean {
    if(this.shoppingCartTotal > +this.customerCreditLimit && this.ShoppingCartForm.get("CreditInvoice").value){
      this.toastr.error('Shopping cart total amount exceeds customer credit limit.');
     return false;
    }
    return true;

  }

  GenerateShoppingCartHeader() {
    this.shoppingCartHeaderService
        .PostShoppingCartHeader(this.SetShoppingCartHeaderObj())
        .subscribe((response) => {
          this.orderNumber = response.id;
          this.Products.value.forEach((product, index) => {
            this.shoppingCartItemService
              .PostShoppingCartItem(
                this.SetShoppingCartItemObj(response.id, index, product)
              )
              .subscribe((res) => {
                console.log(res);
              });
          });
          this.toastr.success('Products loaded in the shopping cart!');
          // After this method, i should call a method that patch value the form with the
          // rowValues getted from the Database for future modifications.
        });
  }

  GenerateOrderOrInvoice() {
    if(this.orderNumber == null || this.orderNumber == undefined) {
    if (confirm('Are you sure you want to generate the order?') && this.ValidateCreditLimit()) {
      this.GenerateShoppingCartHeader();
    }
   } else {
      if (confirm('Are you sure you want to generate the invoice?') && this.ValidateCreditLimit())
      {
        this.GenerateInvoiceHeader();
      }
    }

  }

  SetShoppingCartHeaderObj() {
    return {
      orderNumber: (Math.random() * 1000).toFixed(0), //Generate random order number
      idCustomer: +this.ShoppingCartForm.get('CustomerId').value,
      orderDate: new Date().toISOString(),
      orderAmount: this.shoppingCartTotal.toFixed(2),
      isCredit: this.ShoppingCartForm.get('CreditInvoice').value,
      active: true,
    };
  }

  SetShoppingCartItemObj(orderNumberId, indexRow, product) {
    return {
      idOrder: orderNumberId,
      row: indexRow,
      idProduct: product['ProductCode'],
      description: product['Description'],
      qty: this.Products.value[indexRow]['Qty'],
      price: product['Price'],
      total: +product['Price'] * +this.Products.value[indexRow]['Qty'],
    };
  }

  GenerateInvoiceHeader() {
      this.invoiceHeaderService
        .PostInvoiceHeader(this.SetInvoiceHeaderObj())
        .subscribe((response) => {
          this.Products.value.forEach((product, index) => {
            this.invoiceItemsService
              .PostInvoiceItem(
                this.SetInvoiceItemObj(response.id, index, product, this.orderNumber)
              )
              .subscribe((res) => {
                //Atfer this method , i should updates the products qty
                //this.UpdateProductQty(product, res.rowVersion);
              });

          });
          this.toastr.success('Shopping Invoice Generated!');
          this.Products.clear();
          this.GetProducts();
          this.orderNumber = undefined;
          this.shoppingCartTotal = null;
        })

  }

  SetInvoiceHeaderObj() {
    return {
      invoiceNumber: (Math.random() * 1000).toFixed(0), //Generate random invoice number
      idCustomer: +this.ShoppingCartForm.get('CustomerId').value,
      invoiceDate: new Date().toISOString(),
      invoiceAmount: this.shoppingCartTotal.toFixed(2),
      active: true,
    };
  }

  SetInvoiceItemObj(idInvoice, indexRow, product, orderNumber) {
    return {
      idInvoice: idInvoice,
      row: indexRow,
      idProduct: product['ProductCode'],
      description: product['Description'],
      qty: this.Products.value[indexRow]['Qty'],
      price: product['Price'],
      total: +product['Price'] * +this.Products.value[indexRow]['Qty'],
      orderNumber: orderNumber ,
      orderRow: indexRow,
    };
  }

  UpdateProductQty(product) {

}
}
