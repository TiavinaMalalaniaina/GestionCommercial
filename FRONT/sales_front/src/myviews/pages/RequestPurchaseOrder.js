import React, { useEffect, useState } from 'react';
import {
  CContainer,
  CTable,
  CTableBody,
  CTableHead,
  CTableRow,
  CTableHeaderCell,
  CTableDataCell,
  CButton,
  CListGroup,
  CListGroupItem,
  CCard,
  CCardBody,
  CCardHeader,
} from '@coreui/react';
import API_CONFIG from 'src/apiconfig';

const RequestPurchaseOrder = () => {
  // Liste de tous les produits avec leurs quantités
  const [allProducts, setAllProducts] = useState([]);

  // État pour stocker les produits sélectionnés
  const [selectedProducts, setSelectedProducts] = useState([]);

  // Fonction pour ajouter un produit sélectionné au tableau
  const handleAddProduct = (product) => {
    const productIndex = allProducts.findIndex((p) => p.productId === product.productId);

    if (productIndex !== -1) {
      const updatedProducts = [...allProducts];
      updatedProducts.splice(productIndex, 1);

      setAllProducts(updatedProducts);
      setSelectedProducts([...selectedProducts, product]);
    }
  };

  const handleRemoveProduct = (product) => {
    const updatedList = selectedProducts.filter((p) => p.productId !== product.productId);
    setSelectedProducts(updatedList);

    setAllProducts([...allProducts, product]);
  };

  const handleValidateProducts = () => {
    console.log('Produits validés:', selectedProducts);
    const models = {
      productIds: []
    }
    selectedProducts.forEach(product => {
      models.productIds.push(product.productId)
    });
    const requestOption = {
      method: "POST",
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(models)
    }
    fetch(API_CONFIG.SEND_PURCHASE_ORDER, requestOption)
    .then(res => res.json())
  };

  useEffect(()=>{
    fetch(API_CONFIG.NECESSARY_PRODUCT)
      .then(res => res.json())
      .then(res => setAllProducts(res.data))
  },[])

  return (
    <CContainer>
      <CCard className='mb-4'>
        <CCardHeader>
          <h2>Liste des Produits</h2>
        </CCardHeader>
        <CCardBody>
          <CTable striped>
            <CTableHead>
              <CTableRow>
                <CTableHeaderCell>Product</CTableHeaderCell>
                <CTableHeaderCell>Quantity</CTableHeaderCell>
                <CTableHeaderCell>Action</CTableHeaderCell>
              </CTableRow>
            </CTableHead>
            <CTableBody>
              {allProducts.map((product) => (
                <CTableRow key={product.productId}>
                  <CTableDataCell>{product.productName}</CTableDataCell>
                  <CTableDataCell>{product.quantity}</CTableDataCell>
                  <CTableDataCell>
                    <CButton
                      color="success"
                      onClick={() => handleAddProduct(product)}
                      disabled={selectedProducts.some((p) => p.productId === product.productId)}
                    >
                      {selectedProducts.some((p) => p.productId === product.productId) ? 'Added' : 'Add'}
                    </CButton>
                  </CTableDataCell>
                </CTableRow>
              ))}
            </CTableBody>
          </CTable>
        </CCardBody>
      </CCard>

      <CCard className='mb-4'>
        <CCardHeader>
          <h2>Produits Sélectionnés</h2>
        </CCardHeader>
        <CCardBody>
          <CListGroup>
            {selectedProducts.map((selectedProduct, index) => (
              <CListGroupItem key={index}>
                {selectedProduct.productName}{' --> '}{selectedProduct.quantity}
                <CButton color="danger" onClick={() => handleRemoveProduct(selectedProduct)}>
                  Remove
                </CButton>
              </CListGroupItem>
            ))}
          </CListGroup>

          <CButton color="primary" onClick={handleValidateProducts}>
            Valider les produits
          </CButton>
        </CCardBody>
      </CCard>
    </CContainer>
  );
};

export default RequestPurchaseOrder;
