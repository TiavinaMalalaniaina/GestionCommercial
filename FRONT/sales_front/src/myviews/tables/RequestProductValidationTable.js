import {
    CButton,
    CTable, 
    CTableBody, 
    CTableDataCell, 
    CTableHead, 
    CTableHeaderCell, 
    CTableRow,
    CFormCheck
} from '@coreui/react'

const RequestProductValidationTable = ({data, updateValidated}) => {
    
    const handleCheck =(requestDetailsId, requestId)=> {
        updateValidated(requestDetailsId, requestId)
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
                    <CTableDataCell>{value.product.productName}</CTableDataCell>
                    <CTableDataCell>{value.quantity}</CTableDataCell>
                    <CTableDataCell>{value.reason}</CTableDataCell>
                    <CTableDataCell>
                      <CFormCheck id="checkValidation" label="Validée" name="validated" value={value.requestDetailsId} onChange={()=>handleCheck(value.requestDetailsId, value.requestId) }/>
                    </CTableDataCell>
                </CTableRow>
                )}
            </CTableBody>
        </CTable>
    )
}
export default RequestProductValidationTable