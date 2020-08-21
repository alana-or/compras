class ProductController {
    constructor() {
        this._productService = new ProductService();
        this._productService.listarProdutos();
    }
}
