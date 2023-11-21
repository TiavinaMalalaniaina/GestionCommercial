import React, { useEffect, useMemo, useState } from 'react';
import {
  CCard,
  CCardBody,
  CCardHeader,
  CTable,
  CTableBody,
  CTableDataCell,
  CTableHead,
  CTableRow,
} from '@coreui/react';
import API_CONFIG from 'src/apiconfig';

const ProductsList = () => {
  const [data, setData] = useState([]);

  useEffect(() => {
      fetch(API_CONFIG.REQUEST_DETAILS)
        .then(res => res.json())
        .then(res => {
          const fakeData = []
          res.data.forEach(element => {
            fakeData.push({
              produit: element.product.productName,
              departement: element.departmentName,
              quantite: element.quantity
            })
          });
          console.log(fakeData)
          setData(fakeData)
        })
    // setData(fakeData);
  }, []);

  const produits = useMemo(() => Array.from(new Set(data.map((item) => item.produit))), [data]);
  const departements = useMemo(() => Array.from(new Set(data.map((item) => item.departement))), [data]);

  return (
    <div>
      <CCard>
        <CCardHeader>
          <h2>Tableau Croisé</h2>
        </CCardHeader>
        <CCardBody>
          <CTable striped bordered hover responsive>
            <CTableHead>
              <CTableRow>
                <CTableDataCell>Produit</CTableDataCell>
                {departements.map((departement) => (
                  <CTableDataCell key={departement}>{departement}</CTableDataCell>
                ))}
                <CTableDataCell>Total</CTableDataCell>
              </CTableRow>
            </CTableHead>
            <CTableBody>
              {produits.map((produit) => (
                <CTableRow key={produit}>
                  <CTableDataCell>{produit}</CTableDataCell>
                  {departements.map((departement) => (
                    <CTableDataCell key={departement}>
                      {data.find((item) => item.produit === produit && item.departement === departement)?.quantite || 0}
                    </CTableDataCell>
                  ))}
                  <CTableDataCell>
                    {data
                      .filter((item) => item.produit === produit)
                      .reduce((total, item) => total + (item.quantite || 0), 0)}
                  </CTableDataCell>
                </CTableRow>
              ))}
            </CTableBody>
          </CTable>
        </CCardBody>
      </CCard>
    </div>
  );
};

export default ProductsList;
