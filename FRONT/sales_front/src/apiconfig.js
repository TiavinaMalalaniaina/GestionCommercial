
const BASE_URL = "http://localhost:5020/"
const API_CONFIG={
    ALL_PRODUCT:BASE_URL + "product/get-all-products",
    SEND_REQUEST: BASE_URL + "request/create",
    REQUESTS: BASE_URL + "request/get-all-requests-no-validated",
    REQUEST_VALIDATED: BASE_URL + "request/get-all-requests-validated",
    VALIDATE_REQUEST: BASE_URL + "request/validate",
    SEND_PROFORMA: BASE_URL + "proforma/create",
    PROFORMAS: BASE_URL + "proforma/get-all-proformas",
    SUPPLIERS: BASE_URL + "supplier/get-all-suppliers",
    LOGIN: BASE_URL + "employee/log-in",
    REQUEST_DETAILS: BASE_URL + "request-detail/get-all",
    NECESSARY_PRODUCT: BASE_URL + "product/get-all-necessary-products",
    SEND_PURCHASE_ORDER: BASE_URL + "proforma/create-purchase-order",
    PURCHASE_ORDERS: BASE_URL + "purchase-order/get-all",
    PURCHASE_ORDERS_VALIDATION: BASE_URL + "purchase-order/validate",
    PURCHASE_ORDERS_VALIDATED: BASE_URL + "purchase-order/get-all-validated",
    PURCHASE_ORDERS_NO_VALIDATED: BASE_URL + "purchase-order/get-all-no-validated",
}
export default API_CONFIG
