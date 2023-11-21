import { CCard, CCardBody, CCardTitle } from "@coreui/react"

const SupplierCard=({supplier})=>{
    console.log(supplier)
    if (supplier !== '') {
        return (
            <CCard className="mb-4">
                <CCardBody>
                    <h6>Nom: <strong>{supplier.name}</strong></h6>
                    <h6>Phone: <strong>{supplier.contactPhone}</strong></h6>
                    <h6>Email: <strong>{supplier.contactEmail}</strong></h6>
                    <h6>Addresse: <strong>{supplier.address}</strong></h6>
                </CCardBody>
            </CCard>
            )
    } else {
        return <></>
    }

}
export default SupplierCard