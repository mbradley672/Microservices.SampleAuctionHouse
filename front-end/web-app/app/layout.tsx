import './globals.css'
import type { Metadata } from 'next'
import { Inter } from 'next/font/google'
import Navbar from "@/app/nav/Navbar";
import {Flowbite} from "flowbite-react";
import {defaultFlowbiteTheme} from "@/themes/DefaultFlowbiteTheme";

const inter = Inter({ subsets: ['latin'] })

export const metadata: Metadata = {
  title: 'CarAuctions - Sample',
  description: 'Generated by create next app',
}

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="en">
        <body>
          <Navbar/>
          <main className={'container mx-auto px-5 pt-10'}>
              {children}
          </main>
        </body>
    </html>
  )
}
