import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../../services/product.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-product-add',
  standalone: true,
  // Import ReactiveFormsModule to use form directives
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './product-add.html',
  styleUrl: './product-add.scss',
})
export class ProductAddComponent implements OnInit {
  private fb = inject(FormBuilder);
  private productService = inject(ProductService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  productId: number | null = null;
  isEditMode = false;

  ngOnInit(): void {
    // Check for an 'id' in the URL
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.isEditMode = true;
      this.productId = +idParam; // Convert string to number
      // Fetch the product and populate the form
      this.productService
        .getProductById(this.productId)
        .subscribe((product) => {
          this.productForm.patchValue(product);
        });
    }
  }

  // Create the FormGroup with controls and validators
  productForm = this.fb.group({
    name: ['', Validators.required],
    price: [0, [Validators.required, Validators.min(0.01)]],
  });

  onSubmit() {
    if (this.productForm.invalid) {
      return;
    }

    const productData = this.productForm.value;

    if (this.isEditMode && this.productId) {
      // UPDATE LOGIC
      this.productService
        .updateProduct(this.productId, productData)
        .subscribe(() => {
          this.router.navigate(['/']);
        });
    } else {
      // ADD LOGIC (existing)
      this.productService.addProduct(productData).subscribe(() => {
        this.router.navigate(['/']);
      });
    }
  }
}
