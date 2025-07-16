import { PublicNavbar } from "@/features/navbar/PublicNavbar";
import { HeroSectionOne } from "@/features/Landing/HeroSection";
import { useState } from "react";
import { Outlet } from "react-router-dom";
import Footer from "@/features/footer/Footer";
import HeroSectionTwo from "@/NEW/Pages/HeroSectionTwo";

const LandingPage = () => {
  const [openRegistration, setOpenRegistration] = useState(false);
  const [openLogin, setOpenLogin] = useState(false);
  const [supplyEmail, setSupplyEmail] = useState<string | undefined>(undefined);

  return (
    <>
      <PublicNavbar
        suppliedEmail={supplyEmail}
        setSupplyEmail={setSupplyEmail}
        isAuthenticated={false}
        setOpenRegistration={setOpenRegistration}
        openRegistration={openRegistration}
        openLogin={openLogin}
        setOpenLogin={setOpenLogin}
      />
      <main className=" flex flex-col">
        <HeroSectionOne />
        <HeroSectionTwo />
      
        <Outlet />
      </main>
      <Footer />
    </>
  );
};

export default LandingPage;
