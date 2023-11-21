import {
    CButton,
    CTable, 
    CTableBody, 
    CTableDataCell, 
    CTableHead, 
    CTableHeaderCell, 
    CTableRow
} from '@coreui/react'

import CIcon from '@coreui/icons-react';
import { cilPencil, cilX } from '@coreui/icons';
import { toDictProduct } from 'src/utils/dict';
import { useEffect, useState } from 'react';

const RequestProductTable = ({data, updateProduct, removeProduct, productDict}) => {

    const handleUpdate=(index)=> {
        updateProduct(index)
    }
    const handleRemove=(index)=> {
      console.log(productDict)
        removeProduct(index)
    }
    
    return (
        <CTable striped>
            <CTableHead>
                <CTableRow>
                    <CTableHeaderCell scope="col">#</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Produit</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Quantité</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Motif</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Action</CTableHeaderCell>
                </CTableRow>
            </CTableHead>
            <CTableBody>
                {data.map((value, index) => 
                <CTableRow key={index}>
                    <CTableHeaderCell scope="row">{index+1}</CTableHeaderCell>
                    <CTableDataCell>{value.productId}</CTableDataCell>
                    <CTableDataCell>{value.quantity}</CTableDataCell>
                    <CTableDataCell>{value.reason}</CTableDataCell>
                    <CTableDataCell>
                      <CButton color='primary' onClick={() => handleUpdate(index)}>
                        <CIcon icon={cilPencil} size='sm'/>
                      </CButton>
                      <CButton  color="danger" onClick={() => handleRemove(index)}>
                        <CIcon icon={cilX} size='sm'/>
                      </CButton>
                    </CTableDataCell>
                </CTableRow>
                )}
            </CTableBody>
        </CTable>
    )
}
export default RequestProductTable