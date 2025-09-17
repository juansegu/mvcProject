import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { Product } from '../../../models/product';
import { ProductService } from '../../../services/product.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterModule], // Import CommonModule for *ngFor, currency pipe, etc.
  templateUrl: './product-list.html',
  styleUrl: './product-list.scss',
})
export class ProductListComponent implements OnInit {
  private productService = inject(ProductService);
  products: Product[] = [];
  private cdr = inject(ChangeDetectorRef);

  ngOnInit(): void {
    this.loadProducts();
  }

  onDelete(id: number): void {
    // Use the browser's confirm dialog for a simple confirmation
    if (confirm('Are you sure you want to delete this product?')) {
      this.productService.deleteProduct(id).subscribe(() => {
        // On success, remove the product from the local array to update the UI
        this.products = this.products.filter((p) => p.id !== id);
        this.loadProducts();
      });
    }
  }

  private loadProducts(): void {
    this.productService.getProducts().subscribe((data) => {
      this.products = data;

      this.cdr.detectChanges();
    });
  }
}
