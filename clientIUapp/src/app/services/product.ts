import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Product } from '../models/product';
import { retry } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private http = inject(HttpClient);


  getProducts() {
    return this.http.get<Product[]>('/api/products').pipe(retry(3));
  }
}
