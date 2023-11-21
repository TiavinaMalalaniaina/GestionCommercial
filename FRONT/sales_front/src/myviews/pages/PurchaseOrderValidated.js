import React, { useEffect, useState } from 'react';

import { CContainer, CCard, CCardBody, CCardHeader, CTable, CTableBody, CTableDataCell, CTableHead, CTableRow, CTableHeaderCell, CButton, CCardTitle, CRow, CCol } from '@coreui/react';
import API_CONFIG from 'src/apiconfig';
import { toLetter } from 'src/utils/util';

const PurchaseOrder = ({ order }) => {

  return (
    <CCard className='mb-4'>
      <CCardHeader>
        <h2>Commande d'Achat #{order.purchaseOrderId}</h2>
      </CCardHeader>
      <CCardBody>
        <p><strong>Fournisseur:</strong> {order.supplierId}</p>
        <p><strong>Date de commande:</strong> {order.createdAt}</p>
        <p><strong>Date de livraison prévue:</strong> {order.deliveryDays} jour </p>

        <CTable striped bordered hover responsive>
          <CTableHead>
            <CTableRow>
              <CTableDataCell>Produit</CTableDataCell>
              <CTableDataCell>Quantité</CTableDataCell>
              <CTableDataCell>Prix unitaire</CTableDataCell>
              <CTableDataCell>Prix Hors taxe</CTableDataCell>
            </CTableRow>
          </CTableHead>
          <CTableBody>
            {order.purchaseOrderDetails.map((product, index) => (
              <CTableRow key={index}>
                <CTableDataCell>{product.product.productName}</CTableDataCell>
                <CTableDataCell className='text-end'>{product.quantity.toLocaleString()}</CTableDataCell>
                <CTableDataCell className='text-end'>{product.price.toLocaleString()} Ar</CTableDataCell>
                <CTableDataCell className='text-end'>{(product.quantity * product.price).toLocaleString()} Ar</CTableDataCell>
              </CTableRow>
            ))}
          </CTableBody>
          <CTableBody>
            <CTableRow>
              <CTableHeaderCell  className='text-end' colSpan={3}>Montant total hors taxe</CTableHeaderCell>
              <CTableHeaderCell className='text-end'> {calculateTotal(order.purchaseOrderDetails).toLocaleString()} Ar</CTableHeaderCell>
            </CTableRow>
            <CTableRow>
              <CTableHeaderCell  className='text-end' colSpan={3}>TVA</CTableHeaderCell>
              <CTableHeaderCell  className='text-end'> 20 %</CTableHeaderCell>
            </CTableRow>
            <CTableRow>
              <CTableHeaderCell  className='text-end' colSpan={3}>Montant total TTC</CTableHeaderCell>
              <CTableHeaderCell className='text-end'> {(calculateTotal(order.purchaseOrderDetails) * 1.2).toLocaleString()} Ar</CTableHeaderCell>
            </CTableRow>
          </CTableBody>
        </CTable>
        <p> Montant total arrété à la somme de <strong>{toLetter(calculateTotal(order.purchaseOrderDetails) * 1.2)} Ariary</strong></p>
      </CCardBody>
    </CCard>
  );
};

const calculateTotal = (purchaseOrderDetails) => {
  return purchaseOrderDetails.reduce((total, purchaseOrderDetails) => total + purchaseOrderDetails.quantity * purchaseOrderDetails.price, 0);
};


const PurchaseOrderList = () =>
{
  const [data, setData] = useState([]);

    useEffect(()=>{
      fetch(API_CONFIG.PURCHASE_ORDERS_VALIDATED)
      .then(res => res.json())
      .then(res => {
        console.log(res.data)
        setData(res.data)
      })
    },[])

    return (

        <CContainer>
        <h2>Liste des Commandes Validé</h2>
        {data.map((purchaseOrder, index) => (
            <PurchaseOrder key={index} order={purchaseOrder} />
            ))}
        </CContainer>
    )
}

export default PurchaseOrderList;
