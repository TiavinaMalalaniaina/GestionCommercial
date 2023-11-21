import React from 'react';
import {
  CContainer,
  CCard,
  CCardBody,
  CCardHeader,
  CCardTitle,
  CCardSubtitle,
  CTable,
  CTableBody,
  CTableHead,
  CTableRow,
  CTableHeaderCell,
  CTableDataCell,
  CRow,
} from '@coreui/react';

const ProformaInfo = ({ proforma }) => {
  const calculateTotal = (items) => {
    return items.reduce((total, item) => total + item.quantity * item.price, 0);
  };

  return (
    <CRow>
      <CCard className='mb-4'>
        <CCardHeader>
          <CCardTitle className="text-center">ProformaInfo Invoice</CCardTitle>
          {proforma && (
            <CCardSubtitle className="text-center">
              {proforma.supplier.name} - {proforma.supplier.address}
            </CCardSubtitle>
          )}
        </CCardHeader>
        <CCardBody>
          <CTable striped bordered>
            <CTableHead>
              <CTableRow>
                <CTableHeaderCell>Item</CTableHeaderCell>
                <CTableHeaderCell className='text-end'>Quantity</CTableHeaderCell>
                <CTableHeaderCell className='text-end'>Price per Unit</CTableHeaderCell>
                <CTableHeaderCell className='text-end'>Total</CTableHeaderCell>
              </CTableRow>
            </CTableHead>
            <CTableBody>
              {proforma.proformaDetails.map((item, index) => (
                <CTableRow key={index}>
                  <CTableDataCell>{item.product.productName}</CTableDataCell>
                  <CTableDataCell className='text-end'>{item.quantity.toLocaleString()}</CTableDataCell>
                  <CTableDataCell className='text-end'>{item.price.toLocaleString()} AR</CTableDataCell>
                  <CTableDataCell className='text-end'>{(item.quantity * item.price).toLocaleString()} AR</CTableDataCell>
                </CTableRow>
              ))}
            </CTableBody>
            <CTableBody>
              <CTableRow>
                <CTableHeaderCell colSpan="3" className='text-end'>Total Hors Taxe:</CTableHeaderCell>
                <CTableHeaderCell className='text-end'>{calculateTotal(proforma.proformaDetails).toLocaleString()} AR</CTableHeaderCell>
              </CTableRow>
              <CTableRow>
                <CTableHeaderCell colSpan="3" className='text-end'>Total TTC:</CTableHeaderCell>
                <CTableHeaderCell className='text-end'>{(calculateTotal(proforma.proformaDetails) * 1.2).toLocaleString()} AR</CTableHeaderCell>
              </CTableRow>
            </CTableBody>
          </CTable>
        </CCardBody>
      </CCard>
    </CRow>

  );
};

export default ProformaInfo;
