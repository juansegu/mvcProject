import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Product } from '../models/product';
import { retry } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private http = inject(HttpClient);

  getProducts() {
    return this.http.get<Product[]>('/api/products').pipe(retry(3));
  }

  getProductById(id: number) {
    return this.http.get<Product>(`/api/products/${id}`);
  }

  addProduct(productData: any) {
    // The API returns the newly created product, so we specify Product as the type
    return this.http.post<Product>('/api/products', productData);
  }

  updateProduct(id: number, productData: any) {
    return this.http.put(`/api/products/${id}`, productData);
  }

  deleteProduct(id: number) {
    // The API returns no content, so the observable type is void
    return this.http.delete<void>(`/api/products/${id}`);
  }
}
