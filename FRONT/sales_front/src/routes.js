import React from 'react'

//MyPage
const RequestProduct = React.lazy(() => import('./myviews/pages/RequestProduct'))
const RequestProductValidation = React.lazy(() => import('./myviews/pages/RequestProductValidation'))
const RequestProforma = React.lazy(() => import ('./myviews/pages/RequestProforma'))
const AddProforma = React.lazy(() => import ('./myviews/pages/AddProforma'))
const Proformas = React.lazy(() => import('./myviews/pages/Proforma'))
const RequestPurchaseOrder = React.lazy(() => import('./myviews/pages/RequestPurchaseOrder'))
const PurchaseOrderList = React.lazy(() => import('./myviews/pages/PurchaseOrder'))
const PurchaseOrderListValidated = React.lazy(() => import('./myviews/pages/PurchaseOrderValidated'))
const ProductsList = React.lazy(() => import('./myviews/pages/ProductsList'))

const routes = [
  { path: '/', exact: true, name: 'Home' },
  { path: '/dept/product/request', name: 'RequestProduct', element: RequestProduct },
  { path: '/dept/product/request/validate', name: 'RequestProductValidation', element: RequestProductValidation },
  { path: '/dept/proforma/request', name: 'RequestProforma', element: RequestProforma },
  { path: '/dept/proforma/add', name: 'AddProforma', element: AddProforma },
  { path: '/dept/proformas', name: 'ListProforma', element: Proformas },
  { path: '/dept/purchaseOrder/request', name: 'ListProforma', element: RequestPurchaseOrder },
  { path: '/dept/purchaseOrders', name: 'ListProforma', element: PurchaseOrderList },
  { path: '/dept/purchaseOrders/validated', name: 'ListProforma', element: PurchaseOrderListValidated },
  { path: '/dept/products', name: 'ListProforma', element: ProductsList },
  { path: '/dept/products/sended', name: 'ListProforma', element: ProductsList },
]

export default routes
