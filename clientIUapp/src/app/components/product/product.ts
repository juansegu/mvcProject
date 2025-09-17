import { Component } from '@angular/core';
import { ProductListComponent } from './product-list/product-list';

@Component({
  selector: 'app-product',
  imports: [ProductListComponent],
  templateUrl: './product.html',
  styleUrl: './product.scss',
})
export class ProductComponent {}
