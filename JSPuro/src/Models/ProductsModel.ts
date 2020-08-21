class ProductsModel {

    private _products: ProductModel[] = [];

    add(product: ProductModel): void {

        this._products.push(product);
    }

    toArray(): ProductModel[] {

        return [].concat(this._products);
    }
}
