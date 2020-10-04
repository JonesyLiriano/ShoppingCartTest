import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'ShoppingCart',
    loadChildren: () => import('./Modules/shopping-cart/shopping-cart.module').then(m => m.ShoppingCartModule)
},
  {
    path: 'Customers',
  loadChildren: () => import('./Modules/customer/customer.module').then(m => m.CustomerModule)
},
  {
    path: '', pathMatch: 'full', redirectTo: 'ShoppingCart'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
