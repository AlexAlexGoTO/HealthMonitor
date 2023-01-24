import React, { useState } from 'react';
import { Link, useMatch, useResolvedPath } from "react-router-dom"
import './navbar.css';
import { AlertContext } from '../../alert-context';
import { Alert } from 'react-bootstrap';

export default function Navbar() {
  return (
    <>
          <div className="col-md-8 offset-md-2">
          <nav className="navbar navbar-expand-lg navbar-light">
              <a class="navbar-brand" href="https://medixine.com"> <img className="logo-medix" src="https://medixine.com/wp-content/uploads/medixine-logo-blue.png" alt="Medixine"/> </a>
                  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
                      <span class="navbar-toggler-icon"></span>
                  </button>
                  <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
                      <div class="navbar-nav">
                        <CustomLink to="/">Home</CustomLink>
                        <CustomLink to="/observations">Observations</CustomLink>
                        <CustomLink to="/alerts">
                          Alerts &nbsp;
                          <AlertContext.Consumer>
                            {api => api.badgeNumber > 0 && <span className='alert-badge'> {api.badgeNumber}</span>} 
                          </AlertContext.Consumer>
                        </CustomLink>
                      </div>
                  </div>
          </nav>
      </div>
    </>
  )
}

function CustomLink({ to, children, ...props }) {
    const resolvedPath = useResolvedPath(to)
    const isActive = useMatch({ path: resolvedPath.pathname, end: true })
  
    return (
      <li className={isActive ? "active" : ""}>
        <Link to={to} {...props} className="nav-item nav-link">
          {children}
        </Link>
      </li>
    )
  }