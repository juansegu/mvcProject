import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { Product } from '../../models/product';
import { ProductService } from '../../services/product';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule], // Import CommonModule for *ngFor, currency pipe, etc.
  templateUrl: './product-list.html',
  styleUrl: './product-list.scss',
})
export class ProductListComponent implements OnInit {
  private productService = inject(ProductService);
  products: Product[] = [];
  private cdr = inject(ChangeDetectorRef);
  ngOnInit(): void {
    this.productService.getProducts().subscribe((data) => {
      console.log(data);
      this.products = data;

      this.cdr.detectChanges();
    });
  }
}
