export const toDictSupplier = (suppliers) => {
    let models = []
    suppliers.forEach(supplier => {
        models[supplier.supplierId] = supplier
    });
    return models;
};

export const toDictProduct = (products) => {
    let models = []
    products.forEach(product => {
        models[product.productId] = product
    });
    return models;
};

export const toDictRequest = (requests) => {
    let models = []
    requests.forEach(request => {
        models[request.requestId] = request
    });
    return models;
};