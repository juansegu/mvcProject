import { Routes } from '@angular/router';
import { ProductListComponent } from './components/product/product-list/product-list';
import { ProductAddComponent } from './components/product/product-add/product-add';
import { ProductComponent } from './components/product/product';

export const routes: Routes = [
  { path: 'product', component: ProductComponent },
  { path: 'products', component: ProductListComponent },
  { path: 'products/add', component: ProductAddComponent },
  { path: 'products/edit/:id', component: ProductAddComponent },
];
