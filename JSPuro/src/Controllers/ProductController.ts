class ProductController {
    
    private _productService: ProductService

    constructor() {
        this._productService = new ProductService()
        this._productService.listarProdutos()
    }
}
